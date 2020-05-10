using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    //Strings
    new public string name = "New Item";
    //Sprites
    public Sprite itemIcon = null;
    //Bools
    public bool isDefaultItem = false;

    public string itemDescrip = "New Description";


    public virtual void Use ()
    {
        //UseItem
        //EventHappen

        Debug.Log("Used " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
