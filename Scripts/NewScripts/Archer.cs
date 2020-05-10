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
    public float speed = 1f;
    private float walkCount = 0.0f;
    private float step;
    [Range(0.01f, 1.0f)]
    public float footstepRate = 0.3f;
    const float groundedRadius = 0.2f;
    [Range(0, 0.3f)]
    [SerializeField]
    private float movementSmoothing = 0.05f;
    [SerializeField]
    //LayerMask
    public LayerMask whatToHit;
    public LayerMask whatIsGround;
    //Transform
    public Transform firePoint;
    public Transform endPoint;
    public Transform sightStart, sightEnd;
    private Transform player;
    [SerializeField]
    private Transform groundCheck;
    public Transform archSprite;
    //Bool
    public bool spotted = false;
    public bool knockFromRight;
    private bool hasCollide = false;
    public bool inRange = false;
    private bool canMove = true;
    private bool canAttack = false;
    public bool grounded;
    private bool wasGrounded;
    private bool deathActivate = false;
    [SerializeField]
    //Rigidbody
    private Rigidbody2D archerbody;
    private GameObject playerGameObject;
    public HealthUI healthui;
    //Int
    public int maxHealth = 200;
    int currentHealth;
    public int archerdamage = 20;
    //Animator
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthui.SetMaxHealth(maxHealth);
        archerbody = GetComponent<Rigidbody2D>();
        playerGameObject = GameObject.Find("Player");
        player = playerGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Raycasting();
        Behaviours();
        Shoot();

        if (canAttack == true)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private void FixedUpdate()
    {
        wasGrounded = grounded;
        grounded = false;

        Collider2D[] circleCast = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int circleInt = 0; circleInt < circleCast.Length; circleInt++)
        {
            if (circleCast[circleInt].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }

    void Raycasting()
    {
        //Draws a line that'll detect the player when the player hits the line.
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
        inRange = Physics2D.Linecast(firePoint.position, endPoint.position, 1 << LayerMask.NameToLayer("Player"));
    }

    void Behaviours()
    {
        if (spotted == true)
        {
            //Flips the skeleton sprite.
            if (player.position.x > transform.position.x)
            {
                archSprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                archSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            //Moves the skeleton towards the player and does knockback.
            step = speed * Time.deltaTime;

            if (knockbackCount <= 0)
            {
                Movement();
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
                    if (hasCollide == false && inRange)
                    {
                        hasCollide = true;
                        canAttack = true;
                        StartCoroutine(WaitTime());
                    } 

                }

            }
        }

        if (inRange)
        {
            canMove = false;

        }
        else
        {
            canMove = true;
        }
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Damage");
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

        if (currentHealth <= 0 && deathActivate == false)
        {
            deathActivate = true;
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(Death());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.7f);
        if (inRange == true && canAttack)
        {
            Attack();
        }
        canAttack = false;
        yield return new WaitForSeconds(3);
        hasCollide = false;
    }

    void Attack()
    {
        Debug.Log("Hit Player");
        playerGameObject.GetComponent<PlayerStats>().TakeDamage(archerdamage);
        if (transform.position.x > player.position.x)
        {
            playerGameObject.GetComponent<PlayerStats>().knockFromRight = true;
        }
        else
        {
            playerGameObject.GetComponent<PlayerStats>().knockFromRight = false;
        }

    }

    void Movement()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            walkCount += Time.deltaTime * (speed / 5.0f);
            animator.SetBool("Moving", true);

            if (walkCount > footstepRate)
            {

                walkCount = 0.0f;
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        Debug.Log("Enemy died! ");
        Destroy(gameObject);
    }

}
