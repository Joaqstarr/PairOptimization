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
            //When the player dies, the game pauses so it is not constantly running in the background, preventing the spawning of more enemies and projectiles
            Time.timeScale = 0;
        }

        public void RestartLevel()
        {
            //Unpauses the game when restarting
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}