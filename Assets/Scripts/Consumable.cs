﻿using UnityEngine;
[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public consumableType typeOfConsumable;
 
    public int HPRecover;

    public override void Use()
    {
        base.Use();
        Inventory.Instance.RemoveItem(this, 1);
    }

    public enum consumableType { Potion, Food }
   
}
