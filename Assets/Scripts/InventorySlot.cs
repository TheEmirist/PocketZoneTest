using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Text quantityText;

    Item item;

    public void AddItem(Item newItem, int quantity)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        removeButton.interactable = true;
        quantityText.text = quantity.ToString();
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        quantityText.text = "";
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.RemoveItem(item, 1);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void UpdateSlot(Item newItem, int quantity)
    {
        if (newItem != null)
        {
            AddItem(newItem, quantity);
        }
        else
        {
            ClearSlot();
        }
    }
}
