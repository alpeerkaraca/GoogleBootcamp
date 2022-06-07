using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused;

        public GameObject pauseMenuUI;
        [SerializeField] private GameObject optMenu,
        pauseMenu;

        private void Start()
        {
            GameIsPaused = false;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                    optMenu.SetActive(false);
                    pauseMenu.SetActive(false);
                    
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        public void CloseApp()
        {
            Application.Quit();
        }
    }
}
