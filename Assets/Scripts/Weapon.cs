
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    public weaponType type;
    public int weaponDamage;

    public override void Use()
    {
        base.Use();
    }

    public enum weaponType { Rifle, Psitol, Ammo}
}
