using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LightBossController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private LightBossAnimController animController;
    [SerializeField] private ImpactFrame impactFrame;
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

    [Header("Attack 2 Variables")]

    [SerializeField] private GameObject spearProj;
    [SerializeField] private Vector3 spearSpawnPos;

    [SerializeField] private float spearSpeed;
    [SerializeField] private float Att2JumpX;
    [SerializeField] private float Att2JumpY;

    [Header("Attack 3 Variables")]
    [SerializeField] GameObject Att3Proj;
    [SerializeField] private float Att3JumpX;
    [SerializeField] private float Att3ProjForce;
    [SerializeField] private float Att3SpreadAngle = 30f;
    [SerializeField] private int[] Att3LaunchAngle = {0, 45, 90};
    [SerializeField] private int Att3ProjCount = 3;
    [SerializeField] private float Att3SwingTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<LightBossAnimController>();
    }
    void Start()
    {
        //StartCoroutine("Walk", 1);
        StartCoroutine("Attack3");
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

    private void AttackChooser(int prevAttack = 100)
    {
        int attack = Random.Range(0, 4);
        Debug.Log("Attack Chooser" + attack);
        if (attack == prevAttack)
        {
            Debug.Log("Recursion");
            AttackChooser(prevAttack);
            Debug.Log("Success");
        } else
        {
            switch (attack)
            {
                case 0:
                    if(checkWalk())
                    {
                        attack = Random.Range(1, 3);
                        StartCoroutine("Walk", attack);
                        break;
                    }
                    AttackChooser(prevAttack);
                    break;
                case 1: StartCoroutine("Attack1"); break;
                case 2: StartCoroutine("Attack2"); break;
                case 3: StartCoroutine("Attack3"); break;
            }
        }
    }
    
    private bool checkWalk()
    {
        xDistance = Player.transform.position.x - transform.position.x;

        if (xDistance < 2.5f && xDistance > -2.5f)
        {
            return false;

        }
        return true;
    }
    private IEnumerator Walk(int storedAction)
    {
        yield return new WaitForSeconds(0.5f);
        animController.Walk();

        for(int i = 0; i < 150; i++)
        {
            xDistance = Player.transform.position.x - transform.position.x;
            Debug.Log(xDistance);
            if(xDistance > 2.5f)
            {
                rb.linearVelocityX = walkSpeed;
                Debug.Log("Walk");
                yield return new WaitForFixedUpdate();

            } else if(xDistance < -2.5f)
            {
                rb.linearVelocityX = -walkSpeed;
                Debug.Log("Walk");

                yield return new WaitForFixedUpdate();

            }
            else
            {
                rb.linearVelocityX = 0;
                Debug.Log("Else");
                animController.Idle();
                switch (storedAction)
                {
                    case 1: StartCoroutine("Attack1"); break;
                    case 2: StartCoroutine("Attack2"); break;
                }
                StopCoroutine("Walk");
                yield return new WaitForSeconds(1);
                break;
            }

        }
        Debug.Log("Not Stopped");
        Teleport();
        animController.Idle();
        switch (storedAction)
        {
            case 1: StartCoroutine("Attack1"); break;
            case 2: StartCoroutine("Attack2"); break;
        }

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
        Teleport(direction: true);
        yield return new WaitForSeconds(0.1f);
        canFlip = false;
        yield return new WaitForSeconds(0.3f);
        Att1Swing4.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            if (Att1Swing4.GetComponent<Damage>().hit)
            {
                Att1Swing4.GetComponent<Damage>().hit = false;
                impactFrame.SetImpact(0.25f);
            }
            yield return new WaitForSeconds(0.01f);
        }

        Att1Swing4.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        canFlip = true;
        animController.Idle();
        AttackChooser(1);
    }

    private IEnumerator Attack2()
    {
        float ogGrav = rb.gravityScale;
        float direction = -transform.localScale.x;

        rb.gravityScale = 0f;
        Teleport(direction: true);
        yield return new WaitForSeconds(0.1f);
        animController.Attack2();
        rb.linearVelocity = new Vector2(Att2JumpX * direction, Att2JumpY);
        for (int i = 0; i < 25; i++)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX / 1.1f, rb.linearVelocityY / 1.1f);
            yield return new WaitForSeconds(0.02f);
        }
        rb.linearVelocity = new Vector2(0, 0);
        canFlip = false;
        yield return new WaitForSeconds(0.17f);
        GameObject a = Instantiate(spearProj);
        a.transform.position = new Vector3(transform.position.x + spearSpawnPos.x, transform.position.y + spearSpawnPos.y, 0);
        a.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(Player.transform.position.x - a.transform.position.x, Player.transform.position.y - a.transform.position.y, 0).normalized * spearSpeed;
        yield return new WaitForSeconds(0.25f);
        Teleport(forced: true);
        animController.Idle();
        yield return new WaitForSeconds(0.125f);
        rb.gravityScale = ogGrav;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        yield return new WaitForSeconds(0.125f);
        AttackChooser(2);

    }

    private IEnumerator Attack3()
    {
        Teleport();
        yield return new WaitForSeconds(0.2f);
        float direction = -transform.localScale.x;
        canFlip = false;
        animController.Attack3();
        rb.linearVelocity = new Vector2(Att3JumpX * direction, 0);
        yield return new WaitForSeconds(0.1f);

        int randIndex = Random.Range(0, Att3LaunchAngle.Length);
        float launchAngle = Att3LaunchAngle[randIndex];
        float angleStep = Att3SpreadAngle / (Att3ProjCount - 1);

        float startAngleOffset = -Att3SpreadAngle / 2f;

        for (int i = 0; i < Att3ProjCount; i++)
        {
            float currentAngleOffset = startAngleOffset + (angleStep * i);
            float finalAngle = launchAngle + currentAngleOffset;

            GameObject a = Instantiate(Att3Proj, transform.position, Quaternion.identity);

            Rigidbody2D arb = a.GetComponent<Rigidbody2D>();

            float radianAngle = finalAngle * Mathf.Deg2Rad;
            float xVelocity = Mathf.Cos(radianAngle) * Att3ProjForce;
            float yVelocity = Mathf.Sin(radianAngle) * Att3ProjForce;

            if (Flipped)
            {
                xVelocity *= -1;
            }

            arb.linearVelocity = new Vector2(xVelocity, yVelocity);
            yield return new WaitForSeconds(Att3SwingTime / Att3ProjCount);
        }

        yield return new WaitForSeconds(0.5f);
        canFlip = true;
        animController.Idle();
        AttackChooser(3);



        yield return null;
    }

    private void Teleport(float distance = 1.5f, bool forced = false, bool direction = false)
    {
        StartCoroutine(TeleportCor(distance, forced, direction));
    }
    private IEnumerator TeleportCor(float distance, bool forced, bool direction)
    {
        if (forced)
        {
            xDistance = Player.transform.position.x - transform.position.x;
            if (xDistance >= 0)
            {
                animController.Teleport();

                yield return new WaitForSeconds(0.125f);
                transform.position = new Vector3(Player.transform.position.x - distance, transform.position.y, transform.position.z);
                animController.TeleportReverse();
            }
            else if (xDistance < 0)
            {
                animController.Teleport();

                yield return new WaitForSeconds(0.125f);
                transform.position = new Vector3(Player.transform.position.x + distance, transform.position.y, transform.position.z);
                animController.TeleportReverse();
            }
        } 
        else if(direction)
        {
            xDistance = Player.transform.position.x - transform.position.x;
            if (xDistance > 2)
            {
                animController.Teleport();

                yield return new WaitForSeconds(0.125f);
                transform.position = new Vector3(Player.transform.position.x - distance, transform.position.y, transform.position.z);
                animController.TeleportReverse();
            }
            else if (xDistance < -2)
            {
                animController.Teleport();

                yield return new WaitForSeconds(0.125f);
                transform.position = new Vector3(Player.transform.position.x + distance, transform.position.y, transform.position.z);
                animController.TeleportReverse();
            }
        }
        else
        {
            xDistance = Player.transform.position.x - transform.position.x;
            if (xDistance > 2 || xDistance < -2)
            {
                int i = Random.Range(0, 2);
                if (i == 0)
                {
                    animController.Teleport();

                    yield return new WaitForSeconds(0.125f);
                    transform.position = new Vector3(Player.transform.position.x - distance, transform.position.y, transform.position.z);
                    animController.TeleportReverse();
                }
                else if (i == 1)
                {
                    animController.Teleport();

                    yield return new WaitForSeconds(0.125f);
                    transform.position = new Vector3(Player.transform.position.x + distance, transform.position.y, transform.position.z);
                    animController.TeleportReverse();
                }

            }
        }


    }
    private void Flip()
    {
        Flipped = !Flipped;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
