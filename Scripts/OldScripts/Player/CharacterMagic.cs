using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMagic : MonoBehaviour
{
    //GameObjects
    public GameObject projectile;
    //Transforms
    public Transform shotPoint;
    //Floats
    private float timeBtwShots;
    public float startTimeBtwShots;
    //Ints
    public int maxMana = 100;
    int currentMana;
    int minMana = 25;
    int manaUsage = 25;
    public int regenSpeed = 5;
    //Bools
    bool magicUsable;
    public ManaUI manaUI;

    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        manaUI.SetMaxMana(maxMana);
        InvokeRepeating("Regenerate", 0.0f, 10 / regenSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMana >= minMana)
        {
            magicUsable = true;
        } else
        {
            magicUsable = false;
        }

        if (timeBtwShots <= 0 & magicUsable == true)
        {
            if (Input.GetButtonDown("Magic"))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                currentMana -= manaUsage;
                manaUI.SetMana(currentMana);
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    void Regenerate()
    {
        if (currentMana < maxMana)
        {
            currentMana += 1;
            manaUI.SetMana(currentMana);
        }
    }

}
