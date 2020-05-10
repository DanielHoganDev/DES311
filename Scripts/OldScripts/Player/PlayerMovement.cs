using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    //Floats
    public float horizontalMove = 0f;
    public float runSpeed = 40f;
    //Bools
    bool jump = false;
    public bool swordActive = false;
    public bool magicActive = false;
    public GameObject manaUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        //Test reset button
        if (Input.GetButtonDown("Reset"))
        {
            transform.position = new Vector3(0, 0, 0);
        }
        
        if (swordActive == false)
        {
            gameObject.GetComponent<PlayerCombatScript>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<PlayerCombatScript>().enabled = true;
        }

        
        if (magicActive == false)
        {
            manaUI.SetActive(false);
            gameObject.GetComponent<CharacterMagic>().enabled = false;
        }
        else
        {
            manaUI.SetActive(true);
            gameObject.GetComponent<CharacterMagic>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}
