using UnityEngine;

namespace GravityZones
{
    public class GravityAligner : MonoBehaviour
    {
        [Header("Behavior")]
        [SerializeField] private bool _useRigidbody = true;
        [SerializeField] private bool _instantAlignOnEnable;

        private GravityEntity _gravityEntity;
        private Rigidbody _rb;

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

        public void ResetController()
        {
            if (_rb != null)
                _rb.angularVelocity = Vector3.zero;
        }

        private void Update()
        {
            AlignToGravity();
        }

        private void AlignInstant()
        {
            AlignToGravity();
            
            // If we are snapping instantly, we should also kill any physical spin
            // so the Rigidbody doesn't fight the snap.
            ResetController();
        }
        
        private void AlignToGravity()
        {
            if (_gravityEntity == null) return;

            // 1. Determine which way is "Up"
            Vector3 gravityUp = -_gravityEntity.GetCombinedGravity().normalized;

            // 2. Project our current forward vector onto the new ground plane so it doesn't pitch up/down
            Vector3 projectedForward = Vector3.ProjectOnPlane(transform.forward, gravityUp).normalized;

            // 3. Mathematical Fallback: If we are looking exactly straight down or straight up at the gravity source,
            // the projected forward becomes zero. We use the right axis to calculate a safe forward instead.
            if (projectedForward.sqrMagnitude < 0.001f)
            {
                projectedForward = Vector3.Cross(transform.right, gravityUp).normalized;
            }

            // 4. Apply the rotation: Look forward, while keeping gravityUp as our up direction
            transform.rotation = Quaternion.LookRotation(projectedForward, gravityUp);
        }
    }
}