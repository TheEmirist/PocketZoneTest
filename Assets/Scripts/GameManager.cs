using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Inventory inventory;
    public InventorySaveSystem savesystem;
    public int loadType;
    public Button button;

    void Awake()
    {
        loadType = DataHolder.LoadType;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        if (loadType == 1)
        {
            StartCoroutine(DelayedAction());

            Debug.Log("loaded" + DataHolder.LoadType);
        }
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(0.001f);

        LoadGame();
    }
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        saveData.playerData = new SaveData.PlayerData
        {
            health = PlayerHealth.Instance.currentHealth,
            position = new float[]
            {
                PlayerMovement.Instance.transform.position.x,
                PlayerMovement.Instance.transform.position.y
            }
        };

        foreach (EnemyMovement enemy in GameObject.FindObjectsOfType<EnemyMovement>())
        {
            saveData.enemiesData.Add(new SaveData.EnemyData
            {
                id = enemy.GetEnemyID(),
                health = enemy.GetEnemyHealth(),
                position = new float[] { enemy.transform.position.x, enemy.transform.position.y }
            });
        }

        saveData.inventoryData = inventory.SaveInventoryData();

        SaveSystem.SaveGame(saveData);
    }

    public void LoadGame()
    {
        SaveData saveData = SaveSystem.LoadGame();

        if (saveData != null)
        {
            ApplySaveData(saveData);
            savesystem.LoadInventory();
        }
        else
        {
            Debug.LogError("No saved game data found.");
        }
    }

    public void ApplySaveData(SaveData data)
    {
        PlayerHealth.Instance.currentHealth = data.playerData.health;
        PlayerMovement.Instance.transform.position = new Vector3(data.playerData.position[0], data.playerData.position[1], 0f);

        SaveSystem.LoadEnemies(data.enemiesData);

        inventory.ApplyInventoryData(data.inventoryData);

        Debug.Log("Save data applied!");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
