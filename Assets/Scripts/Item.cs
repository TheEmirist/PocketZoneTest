using UnityEngine;

// BASE ITEM
public class Item : ScriptableObject
{
    public string itemName;
    public int ID;
    public int price;
    public bool Stackable;
    public Sprite itemIcon;

    public virtual void Use()
    {
        //Use item
        //Use the following line if you want to destroy every item after use
        // Inventory.instance.RemoveItem(this, 1);
    }
}
