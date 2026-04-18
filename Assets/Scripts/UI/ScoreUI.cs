using System;
using UnityEngine;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        private float _score = 0;
        [SerializeField]
        private GameObject _player;

        private void Update()
        {
            if (_player != null)
            {
                _score += Time.deltaTime * 10; // Increase score over time, adjust multiplier as needed
            }
            GetComponent<TMPro.TMP_Text>().text = "Score: " + Mathf.FloorToInt(_score);
        }
    }
}