using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenuControl : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 1;
        }

        public void StartButton()
        {
            SceneManager.LoadScene("Anger"); 
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    
        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
