using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    //Floats
    public float radius = 0.2f;
    //Layermasks
    public LayerMask playerLayer;
    //Transforms
    private Transform player;
    private GameObject playerGameObject;
    //Bools
    private bool inRange = false;
    private bool hasInteracted = false;
    private bool isFocus = false;
    private bool groundInteraction = false;
    //Rigidbodys
    Rigidbody2D rigi;

    private void Awake()
    {
        //Gets the rigidbody of the gameobject.
        rigi = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //Finds the player gameobject in scene and sets player to the gameobject's transform.
        playerGameObject = GameObject.Find("Player");
        player = playerGameObject.transform;
    }
    public virtual void Interaction()
    {
        //Gonna be overwritten by the interacted item
    }

    private void Update()
    {

        //Checks is the item is the focus, is in range with the player and it has already been interacted with.
        if (isFocus && inRange && !hasInteracted)
        {
            Interaction();
            hasInteracted = true;
        }
        //Sets the boxcollider of the item to a trigger once it has collider with the ground.
        if (inRange && groundInteraction)
        {
            this.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        DetectPlayer();
    }

    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void DetectPlayer()
    {
        inRange = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

    }
    //Destroys the rigidbody of the item if collider with ground.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            groundInteraction = true;
            Destroy(rigi);
        }
    }
}
