using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public float runSpeed;
    private float activeSpeed; //float - number with decimals

    public bool isGrounded; //bool - true or false
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool canDoubleJump;

    public Animator anim;

    public float knockbackLength, knockbackSpeed;
    private float knockbackCounter;

    public CapsuleCollider2D playerCollider;
    public PhysicsMaterial2D groundedMaterial;
    public PhysicsMaterial2D airMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0f)
        { 

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            if (isGrounded && playerCollider.sharedMaterial != groundedMaterial)
            {
                playerCollider.sharedMaterial = groundedMaterial;
            }
            else if (!isGrounded && playerCollider.sharedMaterial != airMaterial)
            {
                playerCollider.sharedMaterial = airMaterial;
            }

            //theRB.velocity = new Vector2( Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

            if (knockbackCounter <= 0)
            { 

                activeSpeed = moveSpeed;
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    activeSpeed = runSpeed;
                }

                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y); //movement of RB left to right, horizontal from input manager


                if (Input.GetButtonDown("Jump"))
                {
                    if(isGrounded == true)
                    {
                        //theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        Jump();
                        canDoubleJump = true;

                        anim.SetBool("doubleJump", false);
                    }
                    else
                    {
                        if(canDoubleJump == true)
                        {
                            //theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            Jump();
                            canDoubleJump = false;

                            anim.SetTrigger("doDoubleJump");
                        }
                    }
                }

                if(theRB.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;
                }
                if(theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }else
            {
                knockbackCounter -= Time.deltaTime;

                theRB.velocity = new Vector2( knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
            }

            //handles animation
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x)); //Mathf.Abs ignores negtive prefix (-5 becomes 5), makes run animation play when going left
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("ySpeed", theRB.velocity.y);
        }
    }

    public void Jump() //made jump into a function > everytime you have the same line of code in more than one place, make it a function! If you want to add a sound or sparkle effect to the jump you don't have to add it at every instance where the line is used, you can add it here instead
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

        AudioManager.instance.PlaySFXPitched(14);
    }

    public void KnockBack()
    {
        theRB.velocity = new Vector2(0f, jumpForce * .5f);
        anim.SetTrigger("isKnockingBack");

        knockbackCounter = knockbackLength;
    }

    public void BouncePlayer(float bounceAmount)
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceAmount);

        canDoubleJump = true;

        anim.SetBool("isGrounded", true);
    }

}
