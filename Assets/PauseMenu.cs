using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ReturnButtonClicked()
    {
        SceneManager.LoadScene("GameScene"); 
        Time.timeScale = 1; 
    }
    public void MenuButtonClicked()
    {
        SceneManager.LoadScene("Main-Menu-Example"); 
        Time.timeScale = 1;
    }



    public void ExitButtonClicked()
    {
        Application.Quit(); 
    }
}