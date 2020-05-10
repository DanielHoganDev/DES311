using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //Int
    public int maxHealth = 200;
    int currentHealth;
    public int zomdamage = 30;
    //Transform
    public Transform sightStart, sightEnd;
    private Transform player;
    public Transform zomSprite;
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
    private Rigidbody2D zombody;
    private GameObject playerGameObject;
    public HealthUI healthui;
    //AK
    public AK.Wwise.Event death = new AK.Wwise.Event();
    //Animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthui.SetMaxHealth(maxHealth);
        zombody = GetComponent<Rigidbody2D>();
        playerGameObject = GameObject.Find("Player");
        player = playerGameObject.transform;
    }

    private void Update()
    {
        Raycasting();
        Behaviours();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthui.SetHealth(currentHealth);
        knockbackCount = knockbackLength;
        if (player.position.x > transform.position.x)
        {
            knockFromRight = true;
        }
        else
        {
            knockFromRight = false;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        death.Post(gameObject);
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
            animator.SetBool("Spotted", true);
            //Flips the skeleton sprite.
            if (player.position.x > transform.position.x)
            {
                zomSprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                zomSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            //Moves the skeleton towards the player and does knockback.
            float step = speed * Time.deltaTime;
            if (knockbackCount <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            }

        }
        else
        {
            animator.SetBool("Spotted", false);
        }

        if (knockbackCount >= 0)
        {
            Knockback();
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

        }
    }

    void Attack()
    {
        playerGameObject.GetComponent<PlayerStats>().TakeDamage(zomdamage);
        if (transform.position.x > player.position.x)
        {
            playerGameObject.GetComponent<PlayerStats>().knockFromRight = true;
        }
        else
        {
            playerGameObject.GetComponent<PlayerStats>().knockFromRight = false;
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        hasCollide = false;
    }

    void Knockback()
    {
        if (knockFromRight)
            zombody.velocity = new Vector2(-knockback, knockback);
        if (!knockFromRight)
            zombody.velocity = new Vector2(knockback, knockback);
        knockbackCount -= Time.deltaTime;
    }
}
