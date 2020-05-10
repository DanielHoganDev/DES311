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
    float speed = 600;
    //Bools
    bool origiPos = true;
    //Gameobject
    private GameObject player;
    //AkEvents
    public AK.Wwise.Event slamSound = new AK.Wwise.Event();


    private void Awake()
    {
        originalPosition = this.transform.position;
        player = GameObject.Find("Player");
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
        if (collision.gameObject.layer == 10)
        {
            slamSound.Post(gameObject);
            origiPos = false;

        }

        if (collision.gameObject.layer == 9)
        {
            player.GetComponent<PlayerStats>().Die();
        }
    }
}
