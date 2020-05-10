using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable",  menuName = "Inventory/HealthConsumable ")]
public class Consumable : Item
{
    public int increase;

    public GameObject player;

    public override void Use()
    {
        //Finds the player gameobject in scene.
        player = GameObject.Find("Player");
        //Checks whether the player has full health or not. If full nothing will hapen, otherwise it'll use the consumable item and call the player's health regen script using the item's increase amount for the value of restoration.
        if (player.GetComponent<PlayerStats>().currentHealth != player.GetComponent<PlayerStats>().maxHealth.GetValue())
        {
            player.GetComponent<PlayerStats>().Regen(increase);
            base.Use();
            RemoveFromInventory();
        }

    }
}
