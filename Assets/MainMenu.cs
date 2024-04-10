using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("GameScene"); 
        Time.timeScale = 1;
    }

    public void SettingsButtonClicked()
    {
        SceneManager.LoadScene("SettingsScene"); 
    }

    public void ExitButtonClicked()
    {
        Application.Quit(); 
    }
}