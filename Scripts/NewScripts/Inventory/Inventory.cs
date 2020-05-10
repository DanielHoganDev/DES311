using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //List
    public List<Item> items = new List<Item>();
    //Static
    public static Inventory instance;
    //Ints
    public int InventorySpace = 28;
    //Delegate
    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;
    
    private void Awake()
    {
        //Checks if there is already an instance for the gameobject.
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory");
            return;
        }
        //Creates an instance for the inventory
        instance = this;
    }

    public bool Add (Item item)
    {
        //If the item count exceeds the inventory space the script won't allow an item to be added.
        if (items.Count >= InventorySpace)
        {
            return false;
        }
        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
