using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    //Int
    public int maxHealth = 100;
    int currentHealth;
    public int skeledamage = 20;
    //Transform
    public Transform sightStart, sightEnd;
    public Transform player;
    //Bool
    public bool spotted = false;
    public bool knockFromRight;
    private bool hasCollide = false;
    //Float
    public float speed = 1f;
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    //Rigidbody
    private Rigidbody2D skelebody;
    public GameObject playerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        skelebody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Raycasting();
        Behaviours();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockbackCount = knockbackLength;
        if (player.position.x > transform.position.x)
        {
            knockFromRight = true;
        } else
        {
            knockFromRight = false;
        }

        if (currentHealth <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died! ");
        Destroy(gameObject);

    }

    void Raycasting()
    {
        //Draws a line that'll detect the player when the player hits the line.
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
    }

    void Behaviours()
    {
        if (spotted == true)
        {
            //Flips the skeleton sprite.
            if (player.position.x > transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            //Moves the skeleton towards the player and does knockback.
            float step = speed * Time.deltaTime;
            if (knockbackCount <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            } else
            {
                if (knockFromRight)
                    skelebody.velocity = new Vector2(-knockback, knockback);
                if (!knockFromRight)
                    skelebody.velocity = new Vector2(knockback, knockback);
                knockbackCount -= Time.deltaTime;
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (hasCollide == false)
            {
                Attack();
                Debug.Log("Attacked");
                hasCollide = true;
                StartCoroutine(WaitTime());
            }

        } else {

        }
    }

    void Attack()
    {
        playerGameObject.GetComponent<PlayerHealth>().TakeDamage(skeledamage);
        if (transform.position.x > player.position.x)
        {
            playerGameObject.GetComponent<PlayerHealth>().knockFromRight = true;
        } else
        {
            playerGameObject.GetComponent<PlayerHealth>().knockFromRight = false;
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        hasCollide = false;
    }

}
