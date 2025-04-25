using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject pauseUI;
    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //Reset Game
    }
    public void OnGameResumePress()
    {
        pauseUI.SetActive(false);   //Hide
    }
    public void OnGameExitPress()
    {
        Application.Quit();  //Exit Application
    }
    public void OnEnterPausePress()
    {
        pauseUI.SetActive(true);  //UnHide
    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
