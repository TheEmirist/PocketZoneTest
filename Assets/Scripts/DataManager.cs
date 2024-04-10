using UnityEngine;

public class DataManager : MonoBehaviour
{
    public void ApplyDataToGame(SaveData data)
    {
        PlayerHealth.Instance.currentHealth = data.playerData.health;
        PlayerMovement.Instance.transform.position = new Vector3(data.playerData.position[0], data.playerData.position[1], 0f);

        SaveSystem.LoadEnemies(data.enemiesData);

        Inventory.Instance.ApplyInventoryData(data.inventoryData);
    }

    public void SaveData()
    {
        SaveData data = new SaveData();
        data.inventoryData = Inventory.Instance.SaveInventoryData();

        SaveSystem.SaveGame(data);
        Debug.Log("Game data saved.");
    }
}
