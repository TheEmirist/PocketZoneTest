using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public Item item;
    public int quantity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem(item, quantity);
        }
    }

    private void CollectItem(Item item, int quantity)
    {
        if (item != null)
        {
            AddItemToInventory(item, quantity);
            Destroy(gameObject);
        }
    }

   

    private void AddItemToInventory(Item item, int quantity)
    {
        Inventory.Instance.AddItem(item, quantity);
    }
}
