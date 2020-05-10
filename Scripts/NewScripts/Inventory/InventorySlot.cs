using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    //Images
    public Image itemIcon;
    //GameObjects
    public GameObject Buttons;
    private Button itemButton;
    //Texts
    public Text itemInfo;

    public void AddItem (Item newitem)
    {
        //Sets item to the new item being added.
        item = newitem;
        //Sets the item icon for the inventory slot to the item's icon.
        itemIcon.sprite = item.itemIcon;
        //Enables the inventory slot icon so it can be seen.
        itemIcon.enabled = true;
    }

    public void ClearSlot ()
    {
        //Sets the item in the slot to nothing.
        item = null;
        //Sets the inventory slot icon to nothing.
        itemIcon.sprite = null;
        //Disables the icon.
        itemIcon.enabled = false;
        //Disables the button so it can't be pressed.
        Buttons.SetActive (false);
    }

    public void OnRemove ()
    {
        Inventory.instance.Remove(item);
    }

    public void Interact()
    {
        //Enables the button if the item slot contains an item and has been clicked on.
        if (item != null)
        {
            Buttons.SetActive(true);
        }

    }

    //Disables the buttons if the player clicks the back button.
    public void back()
    {
        Buttons.SetActive(false);
    }

    //If there is an item in the slot and the player clicks the use option this will call the item script to activate the use function, then set the buttons back to their disabled state.
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            Buttons.SetActive(false);
        }

    }

    //this is used in order to get the details of an item. If the player hovers over an item the script will change the itemInfo text to whatever the item's text is.
    public void InfoDisplay()
    {
        if (item != null)
        {
            itemInfo.text = item.itemDescrip;
        }

    }

    //This will set the itemInfo text back to nothing once the player stops hovering the mouse over an item. 
    public void InfoReset()
    {
        itemInfo.text = null;
    }

}
