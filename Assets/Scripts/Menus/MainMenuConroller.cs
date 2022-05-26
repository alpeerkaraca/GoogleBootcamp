using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuConroller : MonoBehaviour
{
    public void PressedPlayButtpn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PressedQuitButton()
    {
        print("QUIT!");
        Application.Quit();
    }
    
    
}