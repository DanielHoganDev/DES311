using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //Int.
    int maxHealth;
    int currentHealth;
    public int regenSpeed = 5;
    //Float.
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    //Transform.
    public Transform skeleton;
    public Transform zombie;
    //Bool.
    public bool knockFromRight;
    //Rigidbody.
    private Rigidbody2D rigi;
    public HealthUI healthui;
    //AK
    public AK.Wwise.Event damagetaken = new AK.Wwise.Event();
    //Animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = GetComponent<PlayerStats>().maxHealth.GetValue();
        currentHealth = maxHealth;
        healthui.SetMaxHealth(maxHealth);
        rigi = GetComponent<Rigidbody2D>();
        InvokeRepeating("Regenerate", 0.0f, 10 / regenSpeed);
    }

    private void Update()
    {
        if (knockbackCount <= 0)
        {
            
        }
        else
        {
            if (knockFromRight)
                rigi.velocity = new Vector2(-knockback *4, knockback);
            if (!knockFromRight)
                rigi.velocity = new Vector2(knockback *4, knockback);
            knockbackCount -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        damagetaken.Post(gameObject);
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        healthui.SetHealth(currentHealth);
        knockbackCount = knockbackLength;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            StartCoroutine(DeathAnim());
        }
    }

    public void Die()
    {
        Debug.Log("You died! ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentHealth = maxHealth;

    }

    void Regenerate()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            healthui.SetHealth(currentHealth);
        }
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(1);
        Die();
    }

}
