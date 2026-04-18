using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerHealthEvents : MonoBehaviour
    {
        private Health _healthComponent;
        [SerializeField]
        private CinemachineImpulseSource _onDamagedImpulseSource;
        private void Awake()
        {
            _healthComponent = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _healthComponent.OnHealthChanged += HealthChanged;
            _healthComponent.OnDamaged += Damaged;
            _healthComponent.OnHealed += Healed;
            _healthComponent.OnDead += Dead;
        }


        private void OnDisable()
        {
            _healthComponent.OnHealthChanged -= HealthChanged;
            _healthComponent.OnDamaged -= Damaged;
            _healthComponent.OnHealed -= Healed;
            _healthComponent.OnDead -= Dead;
        }
        
        private void Healed(int newHealth)
        {
        }

        private void Damaged(int newHealth)
        {
            if(_onDamagedImpulseSource)
                _onDamagedImpulseSource.GenerateImpulse();
        }

        private void Dead(int newHealth)
        {
            Destroy(gameObject);
        }

        private void HealthChanged(int newHealth)
        {
        }

    }
}