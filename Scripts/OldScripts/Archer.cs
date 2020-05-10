using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    //Floats
    float timeToFire = 0;
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    //LayerMask
    public LayerMask whatToHit;
    //Transform
    Transform firePoint;
    Transform endPoint;
    public Transform sightStart, sightEnd;
    public Transform player;
    //Bool
    public bool spotted = false;
    public bool knockFromRight;
    private bool hasCollide = false;
    public bool inRange = false;
    public bool deflect = false;
    //Rigidbody
    private Rigidbody2D archerbody;
    public GameObject playerGameObject;
    //Int
    public int maxHealth = 40;
    int currentHealth;
    public int archerdamage = 10;

    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.Find ("FirePoint");
        endPoint = transform.Find("endPoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint!");
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        archerbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Raycasting();
        Behaviours();
        Shoot();

    }

    void Raycasting()
    {
        //Draws a line that'll detect the player when the player hits the line.
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
        inRange = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
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

            if (knockbackCount <= 0)
            {
            }
            else
            {
                if (knockFromRight)
                    archerbody.velocity = new Vector2(-knockback, knockback);
                if (!knockFromRight)
                    archerbody.velocity = new Vector2(knockback, knockback);
                knockbackCount -= Time.deltaTime;
            }
        }

    }

    void Shoot()
    {
        if (spotted == true)
        {
            Vector2 endPointPosition = new Vector2(endPoint.position.x, endPoint.position.y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, endPointPosition - firePointPosition, 8, whatToHit);
            Debug.DrawLine(firePointPosition, endPointPosition, Color.cyan);
            if (hit.collider != null)
            {
                Debug.DrawLine(firePointPosition, hit.point, Color.yellow);
                if (hit.collider.gameObject.layer == 9)
                {
                    if (hasCollide == false)
                    {
                        hasCollide = true;
                        StartCoroutine(WaitTime());
                    }
                }

            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
        Debug.Log("Enemy died! ");
        Destroy(gameObject);

    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        if (inRange == true)
        {
            Attack();
        }
        yield return new WaitForSeconds(1);
        hasCollide = false;
    }

    void Attack()
    {
        Debug.Log("Hit Player");

            playerGameObject.GetComponent<PlayerHealth>().TakeDamage(archerdamage);
            if (transform.position.x > player.position.x)
            {
                playerGameObject.GetComponent<PlayerHealth>().knockFromRight = true;
            }
            else
            {
                playerGameObject.GetComponent<PlayerHealth>().knockFromRight = false;
            }

    }

}
