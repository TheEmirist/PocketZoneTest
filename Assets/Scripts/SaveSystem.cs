using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static void SaveGame(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetSavePath();

        FileStream stream = new FileStream(path, FileMode.Create);

        try
        {
            formatter.Serialize(stream, data);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Failed to serialize data: " + e.Message);
        }
        finally
        {
            stream.Close();
        }
    }

    public static SaveData LoadGame()
    {
        string path = GetSavePath();

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            try
            {
                return (SaveData)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to deserialize data: " + e.Message);
            }
            finally
            {
                stream.Close();
            }
        }
        else
        {
            Debug.Log("No save data found.");
        }

        return null;
    }

    public static void LoadEnemies(List<SaveData.EnemyData> enemiesData)
    {
        foreach (EnemyMovement enemy in GameObject.FindObjectsOfType<EnemyMovement>())
        {
            GameObject.Destroy(enemy.gameObject);
        }

        foreach (SaveData.EnemyData enemyData in enemiesData)
        {
            GameObject enemyObject = GameObject.Instantiate(Resources.Load<GameObject>("EnemyPrefab"));
            EnemyMovement enemy = enemyObject.GetComponent<EnemyMovement>();
            enemy.SetEnemyHealth(enemyData.health);
            enemyObject.transform.position = new Vector3(enemyData.position[0], enemyData.position[1], 0f);
        }
    }

    public static void ApplySaveData(SaveData data)
    {
        PlayerHealth.Instance.currentHealth = data.playerData.health;
        PlayerMovement.Instance.transform.position = new Vector3(data.playerData.position[0], data.playerData.position[1], 0f);

        LoadEnemies(data.enemiesData);

        Inventory.Instance.ApplyInventoryData(data.inventoryData);

        Debug.Log("Save data applied!");
    }

    private static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "saveData.dat");
    }
}
