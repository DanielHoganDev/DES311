using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    //Vector3's
    private Vector3 originalPosition;
    //Rigidbody
    Rigidbody2D slammerRigi;
    //Floats
    float speed = 700;
    //Bools
    bool origiPos = true;
    //Gameobject
    public GameObject player;

    private void Awake()
    {
        originalPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        slammerRigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (origiPos == false)
        {
            slammerRigi.AddForce(Vector2.up * speed * Time.deltaTime);
        }
        if (transform.position.y >= originalPosition.y)
        {
            origiPos = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
        {

            origiPos = false;

        }

        if (collision.gameObject.layer == 9)
        {
            player.GetComponent<PlayerHealth>().Die();
        }
    }
}
