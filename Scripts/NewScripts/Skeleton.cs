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
    private Transform player;
    //Bool
    public bool spotted = false;
    public bool knockFromRight;
    private bool hasCollide = false;
    private bool deathActivate = false;
    private bool canAttack = true;
    private bool canMove = true;
    private bool canStillAttack = true;
    //Float
    public float speed = 1f;
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    private float walkCount = 0.0f;
    private float step;
    [Range(0.01f, 1.0f)]
    public float footstepRate = 0.3f;
    //Rigidbody
    private Rigidbody2D skelebody;
    private GameObject playerGameObject;
    public HealthUI healthui;
    public Transform skeleSprite;
    //Animator
    public Animator animator;

    const float groundedRadius = 0.2f;
    [Range(0, 0.3f)]
    [SerializeField]
    private float movementSmoothing = 0.05f;
    [SerializeField]
    //Bools
    public bool grounded;
    private bool wasGrounded;
    [SerializeField]
    //LayerMasks
    public LayerMask whatIsGround;
    //Transforms
    [SerializeField]
    private Transform groundCheck;

    //Transforms
    public Transform attackPoint;
    //Floats
    public float attackRange = 0.5f;
    //Layermasks
    public LayerMask enemyLayers;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthui.SetMaxHealth(maxHealth);
        skelebody = GetComponent<Rigidbody2D>();
        playerGameObject = GameObject.Find("Player");
        player = playerGameObject.transform;
    }

    private void Update()
    {
        Raycasting();
        Behaviours();
        Attack();
        if (hasCollide == true)
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

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("TakeDamage");
        canStillAttack = false;
        currentHealth -= damage;
        healthui.SetHealth(currentHealth);
        knockbackCount = knockbackLength;
        if (player.position.x > transform.position.x)
        {
            knockFromRight = true;
        } else
        {
            knockFromRight = false;
        }

        if (currentHealth <= 0 && deathActivate == false)
        {
            canStillAttack = false;
            deathActivate = true;
            Die();
        }
    }

    void Die()
    {
         StartCoroutine(Death());
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
            animator.SetBool("Moving", true);
            //Flips the skeleton sprite.
            if (player.position.x > transform.position.x)
            {
                skeleSprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                skeleSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            //Moves the skeleton towards the player and does knockback.
            step = speed * Time.deltaTime;
            if (knockbackCount <= 0)
            {
                Movement();
            } 

        } else
        {
            animator.SetBool("Moving", false);
        }

        if (knockbackCount >= 0)
        {
            Knockback();
        }

    }

    void Attack()
    {

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == ("Player") && canAttack && animator.GetBool("Attack") == false)
            {
                canAttack = false;
                hasCollide = true;
                canMove = false;
                StartCoroutine(WaitTime());

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        if (canStillAttack)
        {
            player.GetComponent<PlayerStats>().TakeDamage(skeledamage);
            if (transform.position.x > player.position.x)
            {
                playerGameObject.GetComponent<PlayerStats>().knockFromRight = true;
            }
            else
            {
                playerGameObject.GetComponent<PlayerStats>().knockFromRight = false;
            }
        }
        yield return new WaitForSeconds(2);
        hasCollide = false;
        canMove = true;
        canStillAttack = true;
        canAttack = true;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        Debug.Log("Enemy died! ");
        Destroy(gameObject);
    }

    void Movement()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            walkCount += Time.deltaTime * (speed / 5.0f);

            if (walkCount > footstepRate)
            {

                walkCount = 0.0f;
            }
        }
    }

    void Knockback()
    {
        if (knockFromRight)
        {
            skelebody.velocity = new Vector2(-knockback, knockback);
        }

        if (!knockFromRight)
        {
            skelebody.velocity = new Vector2(knockback, knockback);
        }
        knockbackCount -= Time.deltaTime;
    }
}
