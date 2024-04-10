using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InventorySaveSystem : MonoBehaviour
{
    public List<Item> itemLibrary = new List<Item>();

    string inventoryString = "";

    public void TransformDataToString()
    {
        foreach (Item item in Inventory.Instance.itemList)
        {
            inventoryString = inventoryString + item.ID + ":" + Inventory.Instance.quantityList[Inventory.Instance.itemList.IndexOf(item)] + "/";
        }
    }

    public void SaveInventory()
    {
        TransformDataToString();
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        InventoryDataNew data = new InventoryDataNew(inventoryString);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadInventory()
    {
        inventoryString = "";
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        InventoryDataNew data = (InventoryDataNew)bf.Deserialize(file);
        file.Close();

        ReadInventoryData(data.inventoryString);

        Inventory.Instance.UpdateInventoryUI();
    }

    public void ReadInventoryData(string data)
    {
        Inventory.Instance.itemList.Clear();
        Inventory.Instance.quantityList.Clear();

        string[] splitData = data.Split('/');

        foreach (string stg in splitData)
        {
            string[] splitID = stg.Split(':');

            if (splitID.Length >= 2)
            {
                int itemIndex = int.Parse(splitID[0]);
                if (itemIndex >= 0 && itemIndex < itemLibrary.Count)
                {
                    Inventory.Instance.itemList.Add(itemLibrary[itemIndex]);
                    Inventory.Instance.quantityList.Add(int.Parse(splitID[1]));
                }
                else
                {
                    Debug.LogError("Invalid item ID in save data: " + itemIndex);
                }
            }
        }
    }
}
