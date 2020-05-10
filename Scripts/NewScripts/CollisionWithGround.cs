using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithGround : MonoBehaviour
{
    public Rigidbody2D rigi;
    // Start is called before the first frame update
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(rigi);
        }
    }
}
