using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void StartLevel()
    {
        DataHolder.LoadType = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void ContinueLevel()
    {
        DataHolder.LoadType = 1;
        SceneManager.LoadScene("GameScene");
        Debug.Log("loadType = " + DataHolder.LoadType);
    }
}
