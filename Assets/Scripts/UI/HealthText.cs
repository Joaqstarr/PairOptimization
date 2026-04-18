using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthText : MonoBehaviour
    {
        [SerializeField] private Health _healthComp; 
        private void Update()
        {
            GetComponent<TMP_Text>().text = "Health: " + _healthComp.currentHealth;
        }
    }
}