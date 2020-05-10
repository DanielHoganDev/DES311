using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    //Text
    public Text healthAmount;
    public Text manaAmount;
    public Text armourAmount;
    public Text meleeDamage;
    public Text manaDamage;
    public Text manaUsage;

    //GameObject
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        healthAmount.text = ("Health: " + player.GetComponent<PlayerStats>().currentHealth + " / " + player.GetComponent<PlayerStats>().maxHealth.GetValue());
        armourAmount.text = ("Armour: " + player.GetComponent<PlayerStats>().armour.GetValue());
        manaAmount.text = ("Mana: " + player.GetComponent<PlayerStats>().currentMana + " / " + player.GetComponent<PlayerStats>().maxMana.GetValue());
        meleeDamage.text = ("Melee Damage: " + player.GetComponent<PlayerStats>().damage.GetValue());
        manaDamage.text = ("Mana Damage: " + player.GetComponent<PlayerStats>().manaDamage.GetValue());
        manaUsage.text = ("Mana Usage: " + player.GetComponent<PlayerStats>().manaUsage.GetValue());
    }
}
