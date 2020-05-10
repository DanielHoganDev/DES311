using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    //Ints
    public int currentHealth { get; private set; }
    public int currentMana { get; private set; }
    public int minMana { get; private set; }
    public int healthAmount;
    public int manaAmount;

    //Stats
    public Stat maxHealth;
    public Stat damage;
    public Stat armour;
    public Stat maxMana;
    public Stat manaUsage;
    public Stat manaDamage;

    //Floats
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    private float timeBtwShots;
    public float startTimeBtwShots;


    //Transform
    public Transform skeleton;
    public Transform zombie;
    public Transform shotPoint;

    //Animator
    public Animator animator;

    //Sprite
    public Transform playerSprite;

    //Rigidbody
    public Rigidbody2D rigi;
    public HealthUI healthui;
    public ManaUI manaUI;

    //Bools
    public bool knockFromRight;
    bool magicUsable;

    //Gameobject
    public GameObject projectile;

    private void Awake()
    {
        currentHealth = maxHealth.GetValue();
        currentMana = maxMana.GetValue();

        healthui.SetMaxHealth(maxHealth.GetValue());
        manaUI.SetMaxMana(maxMana.GetValue());
        minMana = manaUsage.GetValue();
    }

    private void Start()
    {

    }

    public virtual void Update()
    {
        if (knockbackCount <= 0)
        {

        }
        else
        {
            if (knockFromRight)
                rigi.velocity = new Vector2(-knockback * 4, knockback);
            if (!knockFromRight)
                rigi.velocity = new Vector2(knockback * 4, knockback);
            knockbackCount -= Time.deltaTime;
        }

        minMana = manaUsage.GetValue();
        if (currentMana >= minMana)
        {
            magicUsable = true;
        }
        else
        {
            magicUsable = false;
        }

        if (timeBtwShots <= 0 & magicUsable == true)
        {
            if (Input.GetButtonDown("Magic"))
            {
                Instantiate(projectile, shotPoint.position, playerSprite.rotation);
                timeBtwShots = startTimeBtwShots;
                currentMana -= manaUsage.GetValue();
                manaUI.SetMana(currentMana);
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void TakeDamage (int damage)
    {
        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        healthui.SetHealth(currentHealth);
        knockbackCount = knockbackLength;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            StartCoroutine(DeathAnim());
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentHealth = maxHealth.GetValue();
        healthui.SetHealth(currentHealth);
        currentMana = maxMana.GetValue();
        manaUI.SetMana(currentMana);
        transform.position = new Vector3(0f, -0.5f, -2f);
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(1);
        Die();
    }

    public void Regen(int healthAmount)
    {
        Debug.Log("RegeneratioNActivated");
        if (currentHealth < maxHealth.GetValue())
        {
            if (healthAmount > (maxHealth.GetValue() - currentHealth))
            {
                healthAmount = maxHealth.GetValue() - currentHealth;
            }
            Debug.Log("Healed: " + healthAmount);
            currentHealth = currentHealth + healthAmount;
            healthui.SetHealth(currentHealth);
        }

    }

    public void ManaRegen(int manaAmount)
    {
        Debug.Log("ManaRegeneratioNActivated");
        if (currentMana < maxMana.GetValue())
        {
            if (manaAmount > (maxMana.GetValue() - currentMana))
            {
                manaAmount = maxMana.GetValue() - currentMana;
            }
            Debug.Log("Restored: " + manaAmount);
            currentMana = currentMana + manaAmount;
            manaUI.SetMana(currentMana);
        }

    }


}
