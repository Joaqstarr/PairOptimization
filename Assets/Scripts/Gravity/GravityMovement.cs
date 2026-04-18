using UnityEngine;

namespace GravityZones
{
    [DisallowMultipleComponent]
    public class GravityMovement : MonoBehaviour
    {
        // Multiplier to tweak gravity strength applied to this object
        [SerializeField] private float _gravityMultiplier = 1f;

        // If true and a non-kinematic Rigidbody exists, movement will be applied using Rigidbody.MovePosition.
        [SerializeField] private bool _useRigidbodyMovement = true;

        private GravityEntity _gravityEntity;
        private Rigidbody _rb;

        // Accumulates movement inputs provided during the frame and applied in FixedUpdate.
        private Vector3 _accumulatedInput;

        private void Awake()
        {
            _gravityEntity = GetComponent<GravityEntity>();
            _rb = GetComponent<Rigidbody>();

            if (_useRigidbodyMovement && _rb == null)
            {
                _useRigidbodyMovement = false;
            }
        }

        // Input should be in world units per second
        public void AddMovementInput(Vector3 input)
        {
            _accumulatedInput = input;
        }

        private void Update()
        {
            Vector3 gravity = Vector3.zero;
            if (_gravityEntity != null)
            {
                gravity = _gravityEntity.GetCombinedGravity() * _gravityMultiplier;
            }

            Vector3 movement = _accumulatedInput;


            Vector3 delta = (movement + gravity) * Time.deltaTime;

            if (_useRigidbodyMovement && _rb != null && !_rb.isKinematic)
            {
                _rb.linearVelocity = (movement + gravity);
            }
            else
            {
                transform.position += delta;
            }
        }
    }
}