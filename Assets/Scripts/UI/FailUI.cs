using System;
using Player;
using UnityEngine;

namespace UI
{
    public class FailUI : MonoBehaviour
    {
        [SerializeField]
        private Health _healthComp;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _healthComp.OnDead += ShowFailUI;
        }
        private void OnDisable()
        {
            _healthComp.OnDead -= ShowFailUI;
        }

        private void ShowFailUI(int newHealth)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void RestartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}