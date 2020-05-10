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
    public int attackDamage = 40;
    //Gameobject
    //Rigidbody


    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetButtonDown("Deflect"))
        {
            StartCoroutine(Deflect());
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
                enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
            }

            if (enemy.tag == ("Zombie"))
            {
                enemy.GetComponent<Zombie>().TakeDamage(attackDamage);
            }

            if (enemy.tag == ("Archer"))
            {
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

    IEnumerator Deflect()
    {
        gameObject.layer = 11;
        Debug.Log("Deflect");
        yield return new WaitForSeconds(1);
        gameObject.layer = 9;
        Debug.Log("Deflect deactivate");
    }
}
