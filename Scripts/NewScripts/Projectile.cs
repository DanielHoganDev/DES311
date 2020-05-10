using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Floats
    public float speed;
    public float lifeTime;
    //GameObjects
    public GameObject player;
    //Bools
    bool faceCheck;
    //Ints
    private int attackDamage;
    //Layermasks
    public LayerMask enemyLayers;


    private void Awake()
    {
        player = GameObject.Find("Player");
        attackDamage = player.GetComponent<PlayerStats>().manaDamage.GetValue();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        player = GameObject.Find("Player");
        if (player.GetComponent<CharacterController2D>().facingRight == true)
        {
            faceCheck = true;
            Debug.Log("Going Right");
        }
        else
        {
            faceCheck = false;
            Debug.Log("Going Left");
        }
    }

    // Update is called once per frame
    void Update()
    {

        DirectionForce();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void DirectionForce()
    {
        if (faceCheck == true)
        {
            gameObject.transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(-transform.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D enemyContact)
    {
        if (enemyContact.gameObject.layer == 8)
        {
            if (enemyContact.gameObject.tag == ("Skeleton"))
            {
                enemyContact.gameObject.GetComponent<Skeleton>().TakeDamage(attackDamage);
            }

            if (enemyContact.gameObject.tag == ("Zombie"))
            {
                enemyContact.gameObject.GetComponent<Zombie>().TakeDamage(attackDamage);
            }

            if (enemyContact.gameObject.tag == ("Archer"))
            {
                enemyContact.gameObject.GetComponent<Archer>().TakeDamage(attackDamage);
            }
            Destroy(gameObject);
        }

    }
}
