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

    [SerializeField] private bool Flipped;


    [SerializeField] private Vector2 movementInput;

    [Header("Parry")]
    [SerializeField] public bool parry;
    [SerializeField] private float parryTime;
    [SerializeField] private float parryForceX;
    [SerializeField] private float parryForceY;

    [Header("Jump")]
    [SerializeField] private bool jumpInput;
    [SerializeField] public bool isGrounded;
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

    [Header("Attack")]
    [SerializeField] private float attack1Cooldown;
    private bool attackCooldown = false;
    [SerializeField] private GameObject proj1;




    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerManager = GameObject.Find("Player Manager");
        rb = GetComponent<Rigidbody2D>();
        playerMan = playerManager.GetComponent<PlayerManager>();
        inputMan = playerManager.GetComponent<InputManagerPlayer>();
        parryTime = playerMan.parryTime;

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
        }
        else if (movementInput.x < 0)
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

        if (rb.linearVelocityX > 0 && Flipped)
        {
            Flip();
        } else if (rb.linearVelocityX < 0 && !Flipped)
        {
            Flip();
        }
        MovementCheck();
        JumpCheck();
        DashCheck();
        ParryCheck();
        AttackCheck();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.833389f - 0.1648924f, 0.25f - 2.1f), CapsuleDirection2D.Horizontal, 0, whatIsGround);
        if (isGrounded)
        {
            groundJump = true;
            airJumps = trueAirJumps;

        }
        else
        {
            airJump = true;
        }

    }

    private void AttackCheck()
    {
        if(!attackCooldown && inputMan.attackVal)
        {
            Instantiate(proj1, transform.position, Quaternion.identity);
            StartCoroutine(attackTimer());
        }
    }

    private IEnumerator attackTimer()
    {
        attackCooldown = true;
        yield return new WaitForSeconds(attack1Cooldown);
        attackCooldown = false;
    }
    private void ParryCheck()
    {
        if (inputMan.parryVal && playerMan.canParry)
        {
            StartCoroutine("Parry");
        }
    }

    private IEnumerator Parry()
    {
        playerMan.parrying = true;
        playerMan.canParry = false;
        yield return new WaitForSeconds(parryTime);
        playerMan.parrying = false;
        yield return new WaitForSeconds(playerMan.parryDownTime);
        playerMan.canParry = true;
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
        }
        else
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

        }
        else if (!dashInput)
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

    public void parryKnock(bool left)
    {

        if (left)
        {
            rb.linearVelocity = new Vector2(-parryForceX, parryForceY);
        }
        else
        {
            rb.linearVelocity = new Vector2(parryForceX, parryForceY);
        }
    }

    private void Flip()
    {
        Flipped = !Flipped;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
