using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartMission()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextScene);
    }

    public void OpenSettings()
    {
        Debug.Log("Settings Clicked.");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}