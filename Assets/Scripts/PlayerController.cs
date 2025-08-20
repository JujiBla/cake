using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public float runSpeed;
    private float activeSpeed;

    public bool isGrounded;
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

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private bool inputLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        PlayerPrefs.SetString("currentLevel", scene.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f && !inputLocked)
        {

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            if (isGrounded && playerCollider.sharedMaterial != groundedMaterial)
            {
                playerCollider.sharedMaterial = groundedMaterial;
            }
            else if (!isGrounded && playerCollider.sharedMaterial != airMaterial)
            {
                playerCollider.sharedMaterial = airMaterial;
            }

            if (knockbackCounter <= 0)
            {
                activeSpeed = moveSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    activeSpeed = runSpeed;
                }

                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);


                if (Input.GetButtonDown("Jump"))
                {
                    if (coyoteTimeCounter > 0f)
                    {
                        Jump();
                        canDoubleJump = true;
                        anim.SetBool("doubleJump", false);
                    }
                    else if (canDoubleJump)
                    {
                        Jump();
                        canDoubleJump = false;
                        anim.SetTrigger("doDoubleJump");
                    }
                }


                if (theRB.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;
                }
                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
            else
            {
                knockbackCounter -= Time.deltaTime;

                theRB.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
            }

            // handles animation
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("ySpeed", theRB.velocity.y);
        }
    }

    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

        AudioManager.instance.PlaySFXPitched(14);
    }

    public void KnockBack()
    {
        theRB.velocity = new Vector2(0f, jumpForce * 0.5f);
        anim.SetTrigger("isKnockingBack");

        knockbackCounter = knockbackLength;
    }

    public void BouncePlayer(float bounceAmount)
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceAmount);

        canDoubleJump = true;

        anim.SetBool("isGrounded", true);
    }
    

    public void LockInput()
    {
        inputLocked = true;
        theRB.velocity = Vector2.zero;  // Optional: stop movement immediately
        anim.SetFloat("speed", 0);
    }

    public void UnlockInput()
    {
        inputLocked = false;
    }

    public bool IsInputLocked()
    {
        return inputLocked;
    }
}