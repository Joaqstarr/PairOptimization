using System;
using UnityEngine;

namespace Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 4;
        private int _currentHealth;
        public int currentHealth { get => _currentHealth; }
        
        public delegate void OnHealthChanged(int newHealth);
        public event OnHealthChanged onHealthChanged;
        public event OnHealthChanged OnDead;
        
        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth = Math.Clamp(_currentHealth - amount, 0, _maxHealth);
            onHealthChanged?.Invoke(_currentHealth);
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