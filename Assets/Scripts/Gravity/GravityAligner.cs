using UnityEngine;

namespace GravityZones
{
    public class GravityAligner : MonoBehaviour
    {
        [Header("PID Gains")]
        [SerializeField] private float _kp = 10f;
        [SerializeField] private float _ki = 0.5f;
        [SerializeField] private float _kd = 0.5f;

        [Header("Limits")]
        [SerializeField] private float _integralClamp = 1f; // max absolute integral term (radians)
        [SerializeField] private float _maxAngularVelocity = 20f; // rad/s

        [Header("Behavior")]
        [SerializeField] private bool _useRigidbody = true;
        [SerializeField] private bool _instantAlignOnEnable;

        private GravityEntity _gravityEntity;
        private Rigidbody _rb;

        // PID state
        private float _integral;
        private float _lastError; // in radians

        private const float Epsilon = 1e-5f;

        private void Awake()
        {
            _gravityEntity = GetComponent<GravityEntity>();
            _rb = GetComponent<Rigidbody>();

            if (_useRigidbody && _rb == null)
                _useRigidbody = false;
        }

        private void OnEnable()
        {
            // Optionally snap instantly to gravity when enabled
            if (_instantAlignOnEnable)
            {
                AlignInstant();
            }
        }

        // Public: reset the internal PID state (useful when teleporting/spawning)
        public void ResetController()
        {
            _integral = 0f;
            _lastError = 0f;
            if (_rb != null)
                _rb.angularVelocity = Vector3.zero;
        }

        private void Update()
        {
            if (_gravityEntity == null) return;
            transform.up = -_gravityEntity.GetCombinedGravity();
            return;

            Vector3 gravity = _gravityEntity.GetCombinedGravity();
            if (gravity.sqrMagnitude < Epsilon) return; // nothing to align to

            Vector3 targetUp = -gravity.normalized; // object up should oppose gravity
            Vector3 currentUp = transform.up;

            // compute shortest rotation from currentUp to targetUp
            Vector3 axis = Vector3.Cross(currentUp, targetUp);
            float axisMag = axis.magnitude;
            if (axisMag < 1e-6f)
            {
                // Already aligned or exactly opposite - handle small angle or 180deg specially
                float dot = Vector3.Dot(currentUp, targetUp);
                if (dot > 0.999999f) // already aligned
                {
                    // decay integral & exit
                    _integral = 0f;
                    _lastError = 0f;
                    return;
                }
                else
                {
                    // 180 degree flip: pick a fallback axis (use transform.forward)
                    axis = Vector3.Cross(currentUp, transform.forward);
                    axisMag = axis.magnitude;
                    if (axisMag < 1e-6f)
                        axis = Vector3.right; // absolute fallback
                }
            }

            axis /= axisMag; // normalize

            float angleDeg = Vector3.Angle(currentUp, targetUp);
            float error = angleDeg * Mathf.Deg2Rad; // error in radians

            // PID computations
            float dt = Time.fixedDeltaTime;
            // Integral with clamp
            _integral += error * dt;
            _integral = Mathf.Clamp(_integral, -_integralClamp, _integralClamp);

            float derivative = (error - _lastError) / Mathf.Max(dt, 1e-6f);
            _lastError = error;

            // PID output in rad/s (we treat PID output as desired angular velocity magnitude)
            float output = _kp * error + _ki * _integral + _kd * derivative;

            // clamp desired angular velocity magnitude
            float desiredAngVelMag = Mathf.Min(Mathf.Abs(output), _maxAngularVelocity);
            float desiredSign = Mathf.Sign(output);
            Vector3 desiredAngularVelocity = axis * (desiredAngVelMag * desiredSign);

            if (_useRigidbody && _rb != null && !_rb.isKinematic)
            {
                // Apply by directly setting angular velocity for precise control.
                // Optionally we can lerp to smooth changes.
                _rb.angularVelocity = desiredAngularVelocity;
            }
            else
            {
                // Apply rotation directly to transform (not physics-driven).
                // Integrate desired angular velocity to a rotation delta.
                float factor = Mathf.Rad2Deg * dt; // scalar multiplier (deg = rad * Rad2Deg)
                Vector3 deltaDeg = desiredAngularVelocity * factor; // degrees for this step
                float angleStep = deltaDeg.magnitude;
                if (angleStep > 0f)
                {
                    Quaternion deltaRot = Quaternion.AngleAxis(angleStep, axis);
                    transform.rotation = deltaRot * transform.rotation;
                }
            }
        }

        private void AlignInstant()
        {
            if (_gravityEntity == null) return;
            Vector3 gravity = _gravityEntity.GetCombinedGravity();
            if (gravity.sqrMagnitude < Epsilon) return;

            Vector3 targetUp = -gravity.normalized;
            // Rotate so that transform.up == targetUp
            Quaternion rot = Quaternion.FromToRotation(transform.up, targetUp) * transform.rotation;
            if (_useRigidbody && _rb != null && !_rb.isKinematic)
            {
                _rb.MoveRotation(rot);
                _rb.angularVelocity = Vector3.zero;
            }
            else
            {
                transform.rotation = rot;
            }

            // reset controller state after snapping
            ResetController();
        }
    }
}