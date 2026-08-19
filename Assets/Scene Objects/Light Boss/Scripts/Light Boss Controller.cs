using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LightBossController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private LightBossAnimController animController;
    private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float Damage;
    [SerializeField] private float Health;

    [SerializeField] private bool Flipped;
    private bool canFlip;
    [Header("Walk Variables")]
    [SerializeField] private float xDistance;
    [SerializeField] private float walkSpeed;

    [Header("Attack 1 Variables")]
    [SerializeField] private GameObject Att1Swing1;
    [SerializeField] private GameObject Att1Swing2;
    [SerializeField] private GameObject Att1Swing3;
    [SerializeField] private GameObject Att1Swing4;
    [SerializeField] private float Att1JumpX;
    [SerializeField] private float Att1JumpY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<LightBossAnimController>();
    }
    void Start()
    {
        //StartCoroutine("Walk", 1);
        StartCoroutine("Attack1");
        canFlip = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canFlip)
        {
            if (Player.transform.position.x - transform.position.x > 0 && Flipped)
            {
                Flip();
            }
            else if (Player.transform.position.x - transform.position.x < 0 && !Flipped)
            {
                Flip();
            }
        }

    }

    void FixedUpdate()
    {

        
    }

    
    private IEnumerator Walk(int storedAction)
    {
        animController.Walk();
        Debug.Log("Start Coroutine");
        for(int i = 0; i < 150; i++)
        {
            xDistance = Player.transform.position.x - transform.position.x;
            Debug.Log(xDistance);
            if(xDistance > 3.5f)
            {
                rb.linearVelocityX = walkSpeed;
                Debug.Log("Walk");
                yield return new WaitForFixedUpdate();

            } else if(xDistance < -3.5f)
            {
                rb.linearVelocityX = -walkSpeed;
                Debug.Log("Walk");

                yield return new WaitForFixedUpdate();

            }
            else
            {
                Debug.Log("Else");
                switch (storedAction)
                {
                    case 1:
                        
                        break;
                }
                animController.Idle();
                yield return new WaitForFixedUpdate();
            }

        }
        Debug.Log("End");
        animController.Idle();
        //end walking cycle and use an action
    }
    private IEnumerator Attack1()
    {
        float jumpX = Att1JumpX;
        float jumpY = Att1JumpY;
        canFlip = false;
        animController.Attack1();
        yield return new WaitForSeconds(0.5f);
        canFlip = true;

        yield return new WaitForSeconds(0.5f);
        canFlip = false;
        Att1Swing1.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Att1Swing1.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        Teleport();
        canFlip = true;

        yield return new WaitForSeconds(0.6f);
        canFlip = false;
        Att1Swing2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Att1Swing2.SetActive(false);
        canFlip = true;
        yield return new WaitForSeconds(0.15f);

        canFlip = false;
        xDistance = Player.transform.position.x - transform.position.x;
        jumpX = jumpX + Mathf.Abs(xDistance);
        if (Flipped)
        {
            jumpX *= -1;
        }
        rb.linearVelocity = new Vector2(jumpX, jumpY);
        for (int i = 0; i < 3; i++)
        {


            yield return new WaitForSeconds(0.2f);
            Att1Swing3.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Att1Swing3.SetActive(false);
            yield return new WaitForSeconds(0.1f);

        }
        canFlip = true;
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        Teleport();
        yield return new WaitForSeconds(0.1f);
        canFlip = false;
        yield return new WaitForSeconds(0.3f);
        Att1Swing4.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Att1Swing4.SetActive(false);

        canFlip = true;
        yield return null;
    }

    private void Teleport()
    {
        xDistance = Player.transform.position.x - transform.position.x;
        if (xDistance > 2)
        {
            transform.position = new Vector3(Player.transform.position.x - 1.5f, transform.position.y, transform.position.z);
        }
        else if (xDistance < -2)
        {
            transform.position = new Vector3(Player.transform.position.x + 1.5f, transform.position.y, transform.position.z);
        }
    }
    private void Flip()
    {
        Flipped = !Flipped;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
