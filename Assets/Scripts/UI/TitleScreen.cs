using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class TitleScreen : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Gameplay");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}