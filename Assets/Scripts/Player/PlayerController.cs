using GravityZones;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerControls _controls;
        private GravityMovement _gravityMovement;
        private GravityEntity _gravityEntity;

        [SerializeField] private float _speed = 5;
        private void Awake()
        {
            _controls = GetComponent<PlayerControls>();
            _gravityMovement = GetComponent<GravityMovement>();
            _gravityEntity = GetComponent<GravityEntity>();
        }

        private void Update()
        {
            if (_controls == null || _gravityMovement == null) return;
            if (Camera.main == null) return;
            
            Vector3 moveInput = ((Camera.main.transform.up * _controls.moveInput.y) + (Camera.main.transform.right *_controls.moveInput.x)) * _speed;
            _gravityMovement.AddMovementInput(moveInput);

            return;
            Vector3 up = -_gravityEntity.GetCombinedGravity().normalized;
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            Vector3 cameraUp = Camera.main.transform.up;
            if (Vector3.Dot(cameraUp, up) > 0.5)
            {
                cameraUp = Camera.main.transform.forward;
            }

            Vector3 cameraRight = Camera.main.transform.right;
            if (Vector3.Dot(cameraRight, right) > 0.5)
            {
                cameraRight = Camera.main.transform.forward;
            }
            Vector3 projectedUp = Vector3.ProjectOnPlane(cameraUp, up);
            Vector3 projectedUpNorm = projectedUp.normalized;
            Vector3 projectedRight = Vector3.ProjectOnPlane(cameraRight, up);
            Vector3 projectedRightNorm = projectedRight.normalized;
            
            Vector3 input = _controls.moveInput;
            Vector3 move = (projectedUpNorm * input.y + projectedRightNorm * input.x) * _speed;
            

            _gravityMovement.AddMovementInput(move);
        }

    }
}