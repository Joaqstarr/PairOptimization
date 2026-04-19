using System;
using GravityZones;
using UnityEngine;

namespace Enemies
{
    //Now checks for gravity at the start for code to run, instead of constantly checking for it in update
    //Transform is also cached too for optimization
    [RequireComponent(typeof(GravityMovement))]
    public class ProjectileMovement : MonoBehaviour
    {
        private GravityMovement _gravityMovement;
        [SerializeField] private float _speed;
        private Transform _cachedTransform;

        private void Awake()
        {
            _gravityMovement = GetComponent<GravityMovement>();
            _cachedTransform = transform;
        }

        private void Update()
        {
            _gravityMovement.AddMovementInput(_cachedTransform.forward * _speed);
        }
    }
}