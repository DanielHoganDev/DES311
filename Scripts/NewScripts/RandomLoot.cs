using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : Interact
{

    public GameObject[] lootObjects;
    public Sprite mySprite;

    public void DropLoot()
    {

        Vector3 dropPosition = this.transform.position;
        int lootNumber = Random.Range(0, 3);
        int lootNumber2 = Random.Range(0, 3);
        int lootNumber3 = Random.Range(0, lootObjects.Length);

        GameObject loot = (GameObject)Instantiate(lootObjects[lootNumber], dropPosition, Quaternion.identity);
        GameObject loot2 = (GameObject)Instantiate(lootObjects[lootNumber2], dropPosition, Quaternion.identity);
        GameObject loot3 = (GameObject)Instantiate(lootObjects[lootNumber3], dropPosition, Quaternion.identity);
        loot.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-1f, 1f), 2, -2);
        loot2.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-1f, 1f), 2, -2);
        loot3.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-1f, 1f), 2, -2);
    }

    public override void Interaction()
    {
        DropLoot();
        OnDeFocused();
        this.GetComponent<SpriteRenderer>().sprite = mySprite;
        this.GetComponent<RandomLoot>().enabled = false;
    }
}
