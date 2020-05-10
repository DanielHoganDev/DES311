using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    //Integers
    public int armorModifier;
    public int damageModifier;
    public int maxHealthModifier;
    public int maxManaModifier;
    public int manaUsageModifier;
    public int manaDamageModifier;

    public EquipmentSlot equipSlot;

    public void DefaultItem()
    {

    }

    //Calls the equipment manager to equip the specific item and then removes the item fromt the inventory.
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
//Used to tell equipment items apart to be able to equip into different slots.
public enum EquipmentSlot { Head, Chest, Legs, Feet, Weapon, Shield }
