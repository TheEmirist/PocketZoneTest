using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;



    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; 
            pauseMenu.SetActive(true); 
        }
        else
        {
            Time.timeScale = 1; 
            pauseMenu.SetActive(false); 
        }
    }
}
