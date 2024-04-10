using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Inventory : MonoBehaviour
{
    
    public List<Item> itemList = new List<Item>();
    public List<int> quantityList = new List<int>();
    public GameObject inventoryPanel;
    List<InventorySlot> slotList = new List<InventorySlot>();

    #region Singleton

    public static Inventory Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (InventorySlot child in inventoryPanel.GetComponentsInChildren<InventorySlot>())
        {
            slotList.Add(child);
        }
    }

    #endregion

    

    public void AddItem(Item itemAdded, int quantityAdded)
    {
        if (itemAdded.Stackable)
        {
            if (itemList.Contains(itemAdded))
            {
                quantityList[itemList.IndexOf(itemAdded)] += quantityAdded;
            }
            else
            {
                if (itemList.Count < slotList.Count)
                {
                    itemList.Add(itemAdded);
                    quantityList.Add(quantityAdded);
                }
                else { /* Handle case when inventory is full */ }
            }
        }
        else
        {
            for (int i = 0; i < quantityAdded; i++)
            {
                if (itemList.Count < slotList.Count)
                {
                    itemList.Add(itemAdded);
                    quantityList.Add(1);
                }
                else { /* Handle case when inventory is full */ }
            }
        }
        UpdateInventoryUI();
    }

    public void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
        if (itemRemoved.Stackable)
        {
            if (itemList.Contains(itemRemoved))
            {
                int index = itemList.IndexOf(itemRemoved);
                quantityList[index] -= quantityRemoved;

                if (quantityList[index] <= 0)
                {
                    quantityList.RemoveAt(index);
                    itemList.RemoveAt(index);
                }
            }
        }
        else
        {
            for (int i = 0; i < quantityRemoved; i++)
            {
                int index = itemList.IndexOf(itemRemoved);
                quantityList.RemoveAt(index);
                itemList.RemoveAt(index);
            }
        }
        UpdateInventoryUI();
    }

    public Item GetItemById(int itemId)
    {
        foreach (Item item in itemList)
        {
            if (item.ID == itemId)
            {
                return item;
            }
        }
        return null;
    }

    public InventoryData SaveInventoryData()
    {
        List<int> itemIDs = new List<int>();
        List<int> quantities = new List<int>();

        foreach (Item item in itemList)
        {
            itemIDs.Add(item.ID);
            quantities.Add(quantityList[itemList.IndexOf(item)]);
        }

        return new InventoryData(itemIDs, quantities);
    }

    public void ApplyInventoryData(InventoryData data)
    {
        List<Item> newItems = new List<Item>();
        List<int> newQuantities = new List<int>();

        for (int i = 0; i < data.itemIDs.Count; i++)
        {
            int itemID = data.itemIDs[i];
            int quantity = data.quantities[i];

            Item item = GetItemById(itemID);
            if (item != null)
            {
                newItems.Add(item);
                newQuantities.Add(quantity);
            }
        }

        itemList = newItems;
        quantityList = newQuantities;

        UpdateInventoryUI();
    }




    void ClearInventory()
    {
        itemList.Clear();
        quantityList.Clear();
    }

    public void UpdateInventoryUI()
    {
        int ind = 0;

        foreach (InventorySlot slot in slotList)
        {
            if (itemList.Count != 0)
            {
                if (ind < itemList.Count)
                {
                    slot.UpdateSlot(itemList[ind], quantityList[ind]);
                    ind++;
                }
                else
                {
                    slot.UpdateSlot(null, 0);
                }
            }
            else
            {
                slot.UpdateSlot(null, 0);
            }
        }
    }
}
