using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    Item item;

    public Image itemIcon;

    EquipmentManager equipmentManager;


    private void Start()
    {
        //Sets equipmentManager to the EquipmentManager instance.
        equipmentManager = EquipmentManager.instance;
    }

    public void AddItem(Item equipitem)
    {
        item = equipitem;
        //Sets the equip slot icon to the specifc item's icon.
        itemIcon.sprite = item.itemIcon;
        //Enables the icon to be seen
        itemIcon.enabled = true;
    }

    public void ClearSlot()
    {
        //Sets the item to nothing.
        item = null;
        //Removes the item icon sprite.
        itemIcon.sprite = null;
        //Disables the image for the icon.
        itemIcon.enabled = false;
    }

}
