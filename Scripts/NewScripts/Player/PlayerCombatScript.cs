using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    //Transforms
    public Transform attackPoint;
    //Floats
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    //Layermasks
    public LayerMask enemyLayers;
    //Integers
    public int attackDamage;
    //Gameobject
    public GameObject attackSwing;
    //Ak
    public AK.Wwise.Event hit = new AK.Wwise.Event();
    //Animator
    public Animator animator;



    // Update is called once per frame
    void Update()
    {

        attackDamage = GetComponent<PlayerStats>().damage.GetValue();
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                AkSoundEngine.PostEvent("Sword_Swing_Event", attackSwing.gameObject);
                nextAttackTime = Time.time + 3f / attackRate;
                animator.SetTrigger("Attack");
            }
        }

    }

    void Attack()
    {
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == ("Skeleton"))
            {
                hit.Post(gameObject);
                enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
            }

            if (enemy.tag == ("Zombie"))
            {
                hit.Post(gameObject);
                enemy.GetComponent<Zombie>().TakeDamage(attackDamage);
            }

            if (enemy.tag == ("Archer"))
            {
                hit.Post(gameObject);
                enemy.GetComponent<Archer>().TakeDamage(attackDamage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
