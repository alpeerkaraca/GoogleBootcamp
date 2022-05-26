using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartingManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startBtn, exitBtn;


    void Start()
    {
        FadeOut();
    }

    void FadeOut()   
    {
        startBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f); // Bitiş değeri ve ne kadar sürede olacak onu istiyor DOfade

        exitBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);
    }


    public void ExitGame()
    {
        Application.Quit(); 
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("GameScreen"); 
    }
}