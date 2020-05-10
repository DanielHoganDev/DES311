using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickUp : MonoBehaviour
{
    public GameObject playerGameObject;
    //Bools
    bool playerSword = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSword == true) 
        {
            if (Input.GetButtonDown("Attack"))
            {
                Debug.Log("InputDetected");
                playerGameObject.GetComponent<PlayerMovement>().swordActive = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Debug.Log("PlayerEntered");
            playerSword = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Debug.Log("PlayerExit");
            playerSword = false;
        }
    }
}
