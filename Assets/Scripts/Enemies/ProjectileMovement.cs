using System;
using GravityZones;
using UnityEngine;

namespace Enemies
{
    public class ProjectileMovement : MonoBehaviour
    {
        private GravityMovement _gravityMovement;
        [SerializeField] private float _speed;
        private void Awake()
        {
            _gravityMovement = GetComponent<GravityMovement>();
        }

        private void Update()
        {
            if (_gravityMovement)
            {
                _gravityMovement.AddMovementInput(transform.forward * _speed);
            }
        }
    }
}