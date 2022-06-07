using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene("Sadness"); 
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
