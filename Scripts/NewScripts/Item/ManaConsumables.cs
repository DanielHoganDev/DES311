using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/ManaConsumable ")]
public class ManaConsumables : Item
{
    public int increase;

    public GameObject player;

    public override void Use()
    {
        player = GameObject.Find("Player");
        if (player.GetComponent<PlayerStats>().currentMana != player.GetComponent<PlayerStats>().maxMana.GetValue())
        {
            player.GetComponent<PlayerStats>().ManaRegen(increase);
            base.Use();
            RemoveFromInventory();
        }
    }
}
