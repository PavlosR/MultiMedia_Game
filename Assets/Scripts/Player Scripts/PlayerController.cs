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
    [SerializeField] private int airJumps;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;


    [Header("Debug")]
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private bool jumpInput;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canJump;


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
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.833389f - 0.1648924f, 0.25f - 2.1f), CapsuleDirection2D.Horizontal, 0, whatIsGround);
        if (isGrounded )
        {

        }
        JumpCheck();
    }

    private void JumpCheck()
    {
        if(jumpInput && canJump)
        {
            canJump = false;
            if (isGrounded)
            {
                GroundJump();
            }

            else if (airJumps > 0)
            {
                AirJump();
            }
        }
    }

    private void GroundJump()
    {
        if (jumpInput && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce * 0.9f);
            GroundJump();
        }
    }

    private void AirJump()
    {

    }
}
