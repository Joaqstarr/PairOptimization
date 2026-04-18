using System;
using UnityEngine;

namespace Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 4;
        private int _currentHealth;
        public int currentHealth { get => _currentHealth; }
        
        public delegate void OnHealthChangedSignature(int newHealth);
        public event OnHealthChangedSignature OnHealthChanged;
        public event OnHealthChangedSignature OnDead;
        public event OnHealthChangedSignature OnDamaged;
        public event OnHealthChangedSignature OnHealed;
        
        
        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            int oldHealth = _currentHealth;
            _currentHealth = Math.Clamp(_currentHealth - amount, 0, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
            
            if(oldHealth > _currentHealth)
            {
                OnDamaged?.Invoke(_currentHealth);
            }
            else if (oldHealth < _currentHealth)
            {
                OnHealed?.Invoke(_currentHealth);
            }
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
        
        private void Die()
        {
            OnDead?.Invoke(_currentHealth);
        }
    }
}