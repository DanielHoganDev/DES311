using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public GameObject pauseMenu;
    public Equipment startItem;
    // Start is called before the first frame update


    public static PlayerStats instance;

        void Start()
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
        EquipmentManager.instance.onEquipmentChange += OnEquipmentChange;
        damage.AddModifier(startItem.damageModifier);
        pauseMenu = GameObject.Find("PauseMenuCanvas");
        pauseMenu.GetComponent<PauseMenu>().Resume();


    }

    void OnEquipmentChange (Equipment newItem, Equipment olditem)
    {
        if (newItem != null)
        {
            armour.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            maxHealth.AddModifier(newItem.maxHealthModifier);
            maxMana.AddModifier(newItem.maxManaModifier);
            manaUsage.AddModifier(newItem.manaUsageModifier);
            manaDamage.AddModifier(newItem.manaDamageModifier);
        }

        if (olditem != null)
        {
            armour.RemoveModifier(olditem.armorModifier);
            damage.RemoveModifier(olditem.damageModifier);
            maxHealth.RemoveModifier(olditem.maxHealthModifier);
            maxMana.RemoveModifier(olditem.maxManaModifier);
            manaUsage.RemoveModifier(olditem.manaUsageModifier);
            manaDamage.RemoveModifier(olditem.manaDamageModifier);
        }

    }

    public override void Update()
    {
        base.Update();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("End") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            instance = null;
            Destroy(this.gameObject);
        }
    }

}
