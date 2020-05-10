using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{

    Inventory inventory;
    InventorySlot[] slots;
    //Transforms
    public Transform itemsParent;
    //GameObjects
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public GameObject pauseMenu;
    //Instance
    public static InventoryUI instance;

    void Awake()
    {
        //Checks if there is already an instance for the gameobject, if there isn't the gameobject won't be destoryed on load and will create an instance for it.
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        //If there is an instance already it'll destory the new version of the gameobject trying to be created.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Sets inventory to the Inventory instance.
        inventory = Inventory.instance;
        //Adds the UpdateUI function to the onItemChangedCallback
        inventory.onItemChangedCallback += UpdateUI;
        //Gets all of the InventorySlot scripts of the children under the assigned gameobject and slots equals the amount it finds.
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //Finds the PauseMenuCanvas in scene.
        pauseMenu = GameObject.Find("PauseMenuCanvas");
    }

    void Update()
    {
        //If the player presses the button to access the inventory & the inventory is already up this'll do nothing, otherwise it'll set the inventory to active.
        if (Input.GetButtonDown("Inventory"))
        {
            if (pauseMenu.GetComponent<PauseMenu>().paused == true)
            {

            } else
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                equipmentUI.SetActive(!equipmentUI.activeSelf);
            }
        }
        //This disabes the inventory if the player goes to the pause menu while the inventory is open.
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.GetComponent<PauseMenu>().paused == true)
        {
            inventoryUI.SetActive(false);
            equipmentUI.SetActive(false);
        }
        //Checks if the current scene is either the end of the game or the main menu, if the scnene is one of these put the instance back to nothing and destroy the gameobject.
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("End") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            instance = null;
            Destroy(this.gameObject);
        }

    }
    //Used for adding and clearing the number in slots. 
    void UpdateUI ()
    {
        for (int itemAmount = 0; itemAmount < slots.Length; itemAmount++)
        {
            if (itemAmount < inventory.items.Count)
            {
                slots[itemAmount].AddItem(inventory.items[itemAmount]);
            } else
            {
                slots[itemAmount].ClearSlot();
            }
        }
    }
}
