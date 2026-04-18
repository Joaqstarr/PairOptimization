using GravityZones;
using Unity.Cinemachine;
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
            if (_controls == null || _gravityMovement == null || Camera.main == null) return;
            
            Vector3 gravityUp = -_gravityEntity.GetCombinedGravity().normalized;


            Vector3 camForward = Vector3.ProjectOnPlane(Camera.main.transform.up, gravityUp).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(Camera.main.transform.right, gravityUp).normalized;

            Vector3 moveInput = (camForward * _controls.moveInput.y) + (camRight * _controls.moveInput.x);

            moveInput *= _speed;

            _gravityMovement.AddMovementInput(moveInput);
        }

    }
}