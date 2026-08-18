using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Manager")]
    [SerializeField] private GameObject playerManager;
    [SerializeField] private InputManagerPlayer inputMan;
    [SerializeField] private PlayerManager playerMan;

    [Header("Stats")]
    [SerializeField] private int health;
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private float dashStrength;
    [SerializeField] private float moveDamp;
    [SerializeField] private float jumpForce;
    [SerializeField] private int trueAirJumps;

    private Vector3 Velocity = Vector3.zero;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;


    [SerializeField] private Vector2 movementInput;

    [Header("Jump")]
    [SerializeField] private bool jumpInput;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool groundJump;
    [SerializeField] private bool airJump;
    [SerializeField] private int airJumps;
    [SerializeField] private bool canJump;
    [SerializeField, Range(0, 1f)] private float jumpSmoothing;
    [SerializeField] private float airSmoothMultiplier;
    [SerializeField] private bool jumping;

    [Header("Dash")]
    [SerializeField] private bool dashInput;
    [SerializeField] private bool canDash;
    [SerializeField] private float dashCooldown;
    [SerializeField] private bool dashing;
    [SerializeField] private float dashTime;
    [SerializeField] private bool dashHeld;
    [SerializeField] private Vector2 dashDirection;
    [SerializeField] private float dashXMem;


    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerManager = GameObject.Find("Player Manager");
        rb = GetComponent<Rigidbody2D>();
        playerMan = playerManager.GetComponent<PlayerManager>();
        inputMan = playerManager.GetComponent<InputManagerPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        movementInput = inputMan.moveVal;
        jumpInput = inputMan.jumpVal;
        dashInput = inputMan.dashVal;

        dashDirection.y = movementInput.y;

        if (movementInput.x > 0)
        {
            dashXMem = 1;
        } else if (movementInput.x < 0)
        {
            dashXMem = -1;
        }

        if (movementInput == Vector2.zero)
        {
            dashDirection.x = dashXMem;
        }
        else
        {
            dashDirection.x = movementInput.x;
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.833389f - 0.1648924f, 0.25f - 2.1f), CapsuleDirection2D.Horizontal, 0, whatIsGround);
        if (isGrounded)
        {
            groundJump = true;
            airJumps = trueAirJumps;

        } else
        {
            airJump = true;
        }
        MovementCheck();
        JumpCheck();
        DashCheck();
    }

    private void MovementCheck()
    {
        if (!dashing)
        {
            Vector3 targetVelocity = new Vector2(movementInput.x * xSpeed, rb.linearVelocityY);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref Velocity, moveDamp);
        }

    }

    private void JumpCheck()
    {
        if (!dashing)
        {
            if (groundJump)
            {
                GroundJump();
            }

            else if (airJump)
            {
                AirJump();
            }
        }
    }

    private void GroundJump()
    {

        if (isGrounded && jumpInput && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            canJump = false;
            jumping = true;
        }
        else if (jumpInput && rb.linearVelocityY > 0 && jumping == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY);
        }
        else if (!jumpInput || jumping == false)
        {
            if (rb.linearVelocityY > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * jumpSmoothing);

            }

            if (!jumpInput)
            {
                canJump = true;
            }

            jumping = false;

            if (!isGrounded)
            {
                groundJump = false;
            }
        } 

    }

    private void AirJump()
    {

        if (canJump && jumpInput && airJumps > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);

            airJumps -= 1;
            jumping = true;
        }
        if (jumpInput && rb.linearVelocityY > 0 && jumping == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY);
        }
        else 
        {
            if (rb.linearVelocityY > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * jumpSmoothing * airSmoothMultiplier);
            }

            jumping = false;
        }

        if (jumpInput)
        {
            canJump = false;
        } else
        {
            canJump = true;
        }
    }

    private void DashCheck()
    {

        if (dashInput)
        {

            if (canDash && !dashHeld)
            {
                StartCoroutine(Dash());
            }
            dashHeld = true;

        } else if (!dashInput)
        {
            dashHeld = false;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        jumping = false;
        //Dash Code

        rb.linearVelocity = dashDirection * dashStrength;

        yield return new WaitForSeconds(dashTime);
        dashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
}
