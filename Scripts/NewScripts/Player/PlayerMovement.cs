using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    //Floats
    public float horizontalMove = 0f;
    public float runSpeed = 1f;
    private float walkCount = 0.0f;
    [Range(0.01f, 1.0f)]
    public float footstepRate = 0.3f;
    //Bools
    bool jump = false;
    public bool swordActive = false;
    public bool magicActive = false;
    public bool inAir = false;

    //GameObjects
    public GameObject manaUI;
    public GameObject charaControl;

    //AK Events
    public AK.Wwise.Event footstepSound = new AK.Wwise.Event();
    public AK.Wwise.Event jumpSound = new AK.Wwise.Event();
    public AK.Wwise.Event jumpLandSound = new AK.Wwise.Event();
    //Animator
    public Animator animator;
    //Transform
    public Transform interactCheck;
    public Transform interactCheck2;
    public Interact focus;
    //LayerMask
    public LayerMask whatToHit;

    Interact interact;



    // Update is called once per frame
    void Update()
    {

        float speed = runSpeed;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("AirSpeed", Mathf.Abs(horizontalMove));

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumpSound.Post(gameObject);

        }

        if (!controller.grounded)
        {
            inAir = true;
            animator.SetBool("Grounded", false);
        } else
        {
            inAir = false;
            animator.SetBool("Grounded", true);
        }

        if (horizontalMove != 0f && inAir == false)
        {
            walkCount += Time.deltaTime * (speed / 20.0f);

            if (walkCount > footstepRate)
            {
                footstepSound.Post(gameObject);

                walkCount = 0.0f;
            }
        }

        Vector2 endPointPosition = new Vector2(interactCheck2.position.x, interactCheck2.position.y);
        Vector2 firePointPosition = new Vector2(interactCheck.position.x, interactCheck.position.y);
        RaycastHit2D hit = Physics2D.Linecast(firePointPosition, endPointPosition, whatToHit);
        Debug.DrawLine(firePointPosition, endPointPosition, Color.cyan);

        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Pressed E");
            if (hit.collider != null)
            {
                Debug.DrawLine(firePointPosition, hit.point, Color.yellow);
                if (hit.collider.gameObject.layer == 12)
                {
                    interact = hit.collider.gameObject.GetComponent<Interact>();
                    if (interact != null)
                    {
                        SetFocus(interact);
                        StartCoroutine(InteractWaitTime());
                    }

                }
            }
        }

        if (Input.GetButtonDown("Cancel") && focus != null)
        {
            RemoveFocus();
        }

        /*
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
        }*/
    }

    private void FixedUpdate()
    {
        controller.Movement(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    void SetFocus (Interact newFocus)
    {
        if (newFocus != focus)
        {
            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus ()
    {
        focus.OnDeFocused();
        focus = null;
    }

    IEnumerator InteractWaitTime() 
    {
        yield return new WaitForSeconds(0.1f);
        interact = null;

    }





}
