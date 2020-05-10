using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    //Floats
    [SerializeField]
    private float jumpForce = 200f;
    const float groundedRadius = 0.2f;
    [Range(0, 0.3f)]
    [SerializeField]
    private float movementSmoothing = 0.05f;
    [SerializeField]
    //Bools
    private bool inAirControl = false;
    public bool grounded;
    public bool facingRight = true;
    private bool wasGrounded;
    [SerializeField]
    //LayerMasks
    public LayerMask whatIsGround;
    //Transforms
    [SerializeField]
    private Transform groundCheck;
    //RigidBody
    private Rigidbody2D rigi;
    //Vector3
    private Vector3 velocity = Vector3.zero;
    //Gameobjects
    public GameObject charaSprite;
 
    //AKSwitch
    [SerializeField] private AK.Wwise.Switch materialSwitchSand;
    [SerializeField] private AK.Wwise.Switch materialSwitchDirt;
    [SerializeField] private AK.Wwise.Switch materialSwitchGrass;
    //UnityEvents
    public UnityEvent OnLandEvent;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
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
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
                foreach (Collider2D floor in circleCast)
                {
                    if (floor.tag == ("Sand"))
                    {
                        materialSwitchSand.SetValue(this.gameObject);
                    }

                    if (floor.tag == ("Dirt"))
                    {
                        materialSwitchDirt.SetValue(this.gameObject);
                    }

                    if (floor.tag == ("Grass"))
                    {
                        materialSwitchGrass.SetValue(this.gameObject);
                    }
                }
            }
        }
    }

    public void Movement(float move, bool crouch, bool jump)
    {
        if (grounded || inAirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, rigi.velocity.y);

            rigi.velocity = Vector3.SmoothDamp(rigi.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (move > 0 && charaSprite.GetComponent<SpriteFlip>().facingRight == false)
            {
                charaSprite.GetComponent<SpriteFlip>().Flip();
                facingRight = charaSprite.GetComponent<SpriteFlip>().facingRight;

            }
            else if (move < 0 && charaSprite.GetComponent<SpriteFlip>().facingRight == true)
            {
                charaSprite.GetComponent<SpriteFlip>().Flip();
                facingRight = charaSprite.GetComponent<SpriteFlip>().facingRight;
            }
        }

        if (grounded && jump)
        {
            grounded = false;
            rigi.AddForce(new Vector2(0f, jumpForce));
        }
    }


}
