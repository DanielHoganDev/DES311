using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public Equipment[] currentEquipment;

    Inventory inventory;

    public Equipment[] defaultItems;
    private GameObject inventoryUI;
    private GameObject equipSlot;
    public int slotIndex;
    EquipSlot[] slots;
    public Transform equipmentParent;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment olditem);
    public OnEquipmentChange onEquipmentChange;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        inventoryUI = GameObject.Find("InventoryCanvas");
        equipSlot = GameObject.Find("EquipSlot");

    }

    private void Start()
    {
        inventory = Inventory.instance;
        int numofSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = defaultItems;

        slots = equipmentParent.GetComponentsInChildren<EquipSlot>();


    }

    public void Equip (Equipment newItem)
    {
        slotIndex = (int)newItem.equipSlot;

        Equipment olditem = null;

        if (currentEquipment[slotIndex] != null)
        {
            olditem = currentEquipment[slotIndex];
            inventory.Add(olditem);
        }

        if (onEquipmentChange != null)
        {
            onEquipmentChange.Invoke(newItem, olditem);
        }

        currentEquipment[slotIndex] = newItem;

        slots[slotIndex].AddItem(newItem);
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment olditem = currentEquipment[slotIndex];
            inventory.Add(olditem);
            currentEquipment[slotIndex] = null;

            if (onEquipmentChange != null)
            {
                onEquipmentChange.Invoke(null, olditem);
            }
        }
    }

    public void UnequipAll ()
    {
        for (int equiped = 0; equiped < currentEquipment.Length; equiped++)
        {
            Unequip(equiped);
            slots[equiped].ClearSlot();
        }
    }

    private void Update()
    {
        //When this button is pressed activates the UnequipAll void.
        if (Input.GetButtonDown("Unequip"))
        {
            UnequipAll();
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("End") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            instance = null;
            Destroy(this.gameObject);
        }

    }
}
