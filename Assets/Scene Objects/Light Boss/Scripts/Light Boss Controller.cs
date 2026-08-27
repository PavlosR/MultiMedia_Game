using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LightBossController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private LightBossAnimController animController;
    [SerializeField] private ImpactFrame impactFrame;

    [SerializeField] private CinemachineVirtualCamera virtualCam;
    private Rigidbody2D rb;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip swordSwing;
    [SerializeField] private AudioClip swordSwing2;
    [SerializeField] private AudioClip swordSwing3;
    [SerializeField] private AudioClip spearThrow;
    
    
    
    [Header("Stats")]
    [SerializeField] private float Damage;
    [SerializeField] public float Health;

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

    [Header("Attack 4 Variables")]
    [SerializeField] GameObject Att4Proj;
    [SerializeField] float Att4TpDist;
    [SerializeField] float Att4ZoomAmount;
    [SerializeField] float Att4ZoomSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<LightBossAnimController>();
    }
    void Start()
    {
        //StartCoroutine("Walk", 1);
        StartCoroutine("Spawn");
        canFlip = true;
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5f);
        AttackChooser();
    }
    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            Die();
        }
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
        int attack = Random.Range(0, 13);

        if (prevAttack == 1 && attack >= 1 && attack <= 5)
        {
            AttackChooser(prevAttack);
            return;
        }
        else if (prevAttack == 6 && attack >= 6 && attack <= 8)
        {
            AttackChooser(prevAttack);
            return;
        }
        else if (prevAttack == 9 && attack >= 9 && attack <= 11)
        {
            AttackChooser(prevAttack);
            return;
        }
        if (attack == prevAttack)
        {
            AttackChooser(prevAttack);
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
                case 1: StartCoroutine("Attack1", 0); break;
                case 2: StartCoroutine("Attack1", 1); break;
                case 3: StartCoroutine("Attack1", 2); break;
                case 4: StartCoroutine("Attack1", 3); break;
                case 5: StartCoroutine("Attack1", 4); break;
                case 6: StartCoroutine("Attack2", 1); break;
                case 7: StartCoroutine("Attack2", 2); break;
                case 8: StartCoroutine("Attack2", 3); break;
                case 9: StartCoroutine("Attack3", 1); break;
                case 10: StartCoroutine("Attack3", 2); break;
                case 11: StartCoroutine("Attack3", 3); break;
                case 12: StartCoroutine("Attack4"); break;
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
            if(xDistance > 2.5f)
            {
                rb.linearVelocityX = walkSpeed;
                yield return new WaitForFixedUpdate();

            } else if(xDistance < -2.5f)
            {
                rb.linearVelocityX = -walkSpeed;

                yield return new WaitForFixedUpdate();

            }
            else
            {
                rb.linearVelocityX = 0;
                animController.Idle();
                switch (storedAction)
                {
                    case 1: StartCoroutine("Attack1", 0); break;
                    case 2: StartCoroutine("Attack1", 1); break;
                    case 3: StartCoroutine("Attack1", 2); break;
                    case 4: StartCoroutine("Attack1", 3); break;
                    case 5: StartCoroutine("Attack1", 4); break;
                    case 6: StartCoroutine("Attack2", 1); break;
                    case 7: StartCoroutine("Attack2", 2); break;
                    case 8: StartCoroutine("Attack2", 3); break;
                    case 9: StartCoroutine("Attack3", 1); break;
                    case 10: StartCoroutine("Attack3", 2); break;
                    case 11: StartCoroutine("Attack3", 3); break;
                    case 12: StartCoroutine("Attack4"); break;
                }
                StopCoroutine("Walk");
                yield return new WaitForSeconds(1);
                break;
            }

        }
        Teleport();
        animController.Idle();
        switch (storedAction)
        {
            case 1: StartCoroutine("Attack1", 0); break;
            case 2: StartCoroutine("Attack1", 1); break;
            case 3: StartCoroutine("Attack1", 2); break;
            case 4: StartCoroutine("Attack1", 3); break;
            case 5: StartCoroutine("Attack1", 4); break;
            case 6: StartCoroutine("Attack2", 1); break;
            case 7: StartCoroutine("Attack2", 2); break;
            case 8: StartCoroutine("Attack2", 3); break;
            case 9: StartCoroutine("Attack3", 1); break;
            case 10: StartCoroutine("Attack3", 2); break;
            case 11: StartCoroutine("Attack3", 3); break;
            case 12: StartCoroutine("Attack4"); break;
        }

        //end walking cycle and use an action
    }
    private IEnumerator Attack1(int count)
    {
        float jumpX = Att1JumpX;
        float jumpY = Att1JumpY;
        canFlip = false;
        animController.Attack1(count);
        AudioSource.PlayClipAtPoint(swordSwing, transform.position);
        yield return new WaitForSeconds(0.5f);
        if (count  <= 0)
        {

            yield return new WaitForSeconds(0.25f);
            Teleport(forced: true, opposite: true);
            yield return new WaitForSeconds(0.125f);
        }
        else if (count >= 1)
        {
            canFlip = true;

            yield return new WaitForSeconds(0.5f);
            canFlip = false;
            AudioSource.PlayClipAtPoint(swordSwing, transform.position);
            Att1Swing1.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Att1Swing1.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            if (count >= 2)
            {
                Teleport();
                canFlip = true;

                yield return new WaitForSeconds(0.6f);
                canFlip = false;
                AudioSource.PlayClipAtPoint(swordSwing, transform.position);
                Att1Swing2.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                Att1Swing2.SetActive(false);
                canFlip = true;
                if (count >= 3)
                {
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

                        AudioSource.PlayClipAtPoint(swordSwing2, transform.position);
                        yield return new WaitForSeconds(0.2f);
                        Att1Swing3.SetActive(true);
                        yield return new WaitForSeconds(0.1f);
                        Att1Swing3.SetActive(false);
                        yield return new WaitForSeconds(0.1f);

                    }
                    canFlip = true;
                    rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
                    if (count >= 4)
                    {
                        Teleport(direction: true);
                        yield return new WaitForSeconds(0.1f);
                        canFlip = false;
                        yield return new WaitForSeconds(0.3f);
                        Att1Swing4.SetActive(true);
                        AudioSource.PlayClipAtPoint(swordSwing3, transform.position);
                        impactFrame.SetImpact(0.25f);

                        yield return new WaitForSeconds(0.1f);

                        Att1Swing4.SetActive(false);
                        yield return new WaitForSeconds(0.5f);
                    }

                }

            }

        }


        canFlip = true;
        animController.Idle();
        AttackChooser(1);
    }

    private IEnumerator Attack2(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float ogGrav = rb.gravityScale;
            float direction = -transform.localScale.x;

            rb.gravityScale = 0f;
            Teleport(direction: true);
            yield return new WaitForSeconds(0.1f);
            animController.Attack2();
            rb.linearVelocity = new Vector2(Att2JumpX * direction, Att2JumpY);
            for (int j = 0; j < 25; j++)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX / 1.1f, rb.linearVelocityY / 1.1f);
                yield return new WaitForSeconds(0.02f);
            }
            rb.linearVelocity = new Vector2(0, 0);
            canFlip = false;
            yield return new WaitForSeconds(0.17f);
            AudioSource.PlayClipAtPoint(spearThrow, transform.position);
            GameObject a = Instantiate(spearProj);
            a.transform.position = new Vector3(transform.position.x + spearSpawnPos.x, transform.position.y + spearSpawnPos.y, 0);
            a.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(Player.transform.position.x - a.transform.position.x, Player.transform.position.y - a.transform.position.y, 0).normalized * spearSpeed;
            yield return new WaitForSeconds(0.25f);
            Teleport(forced: true, opposite: true);
            animController.Idle();
            yield return new WaitForSeconds(0.125f);
            canFlip = true;
            rb.gravityScale = ogGrav;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            yield return new WaitForSeconds(0.125f);
        }

        AttackChooser(6);

    }

    private IEnumerator Attack3(int count)
    {
        Teleport();
        yield return new WaitForSeconds(0.2f);
        float direction = -transform.localScale.x;
        canFlip = false;
        animController.Attack3();
        rb.linearVelocity = new Vector2(Att3JumpX * direction, 0);
        yield return new WaitForSeconds(0.1f);

        int randIndex = count - 1;
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
        AttackChooser(9);
    }

    private IEnumerator Attack4()
    {
        float ogOrth = virtualCam.m_Lens.OrthographicSize;
        StartCoroutine(Zoom(Att4ZoomSpeed, Att4ZoomAmount));
        yield return new WaitForSeconds(0.25f);
        Teleport(Att4TpDist, above: true);
        float ogGrav = rb.gravityScale;
        rb.gravityScale = 0f;
        canFlip = false;
        yield return new WaitForSeconds(0.125f);
        animController.Attack4();

        yield return new WaitForSeconds(0.5f);

        GameObject a = Instantiate(Att4Proj, transform.position, Quaternion.identity);
        BigBeamSweapPortal sweapPortal = a.GetComponent<BigBeamSweapPortal>();

        yield return new WaitForSeconds(sweapPortal.StartTime + (3.05f * sweapPortal.attackCount) + 1f);
        StartCoroutine(Zoom(Att4ZoomSpeed, ogOrth));
        yield return new WaitForSeconds(0.5f);
        Teleport(Att4TpDist, below:true);
        yield return new WaitForSeconds(0.25f);
        canFlip = true;
        animController.Idle();
        rb.gravityScale = ogGrav;
        AttackChooser(12);


    }

    private void Teleport(float distance = 1.5f, bool forced = false, bool direction = false, bool above = false, bool below = false, bool opposite = false)
    {
        StartCoroutine(TeleportCor(distance, forced, direction, above, below, opposite));
    }
    private IEnumerator TeleportCor(float distance, bool forced, bool direction, bool above, bool below, bool opposite)
    {

        if (above)
        {
            animController.Teleport();

            yield return new WaitForSeconds(0.125f);
            transform.position = new Vector3(Player.transform.position.x, transform.position.y + distance, transform.position.z);
            animController.TeleportReverse();

        } else if (below)
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                animController.Teleport();

                yield return new WaitForSeconds(0.125f);
                transform.position = new Vector3(Player.transform.position.x - 1.5f, transform.position.y - distance, transform.position.z);
                animController.TeleportReverse();
            }
            else if (i == 1)
            {
                animController.Teleport();

                yield return new WaitForSeconds(0.125f);
                transform.position = new Vector3(Player.transform.position.x + 1.5f, transform.position.y - distance, transform.position.z);
                animController.TeleportReverse();
            }
        }
        else if (forced)
        {
            xDistance = Player.transform.position.x - transform.position.x;
            if (opposite)
            {
                if (xDistance >= 0)
                {
                    animController.Teleport();

                    yield return new WaitForSeconds(0.125f);
                    transform.position = new Vector3(Player.transform.position.x + distance, transform.position.y, transform.position.z);
                    animController.TeleportReverse();
                }
                else if (xDistance < 0)
                {
                    animController.Teleport();

                    yield return new WaitForSeconds(0.125f);
                    transform.position = new Vector3(Player.transform.position.x - distance, transform.position.y, transform.position.z);
                    animController.TeleportReverse();
                }
            } 
            else
            {
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

    private IEnumerator Zoom(float Speed, float Amount)
    {
            for (int i = 0; i < 100; i++)
            {
                LensSettings lens = virtualCam.m_Lens;

                lens.OrthographicSize += ((Amount - virtualCam.m_Lens.OrthographicSize) / Speed);
                virtualCam.m_Lens = lens;
                yield return new WaitForSeconds(0.01f);
            }
            virtualCam.m_Lens.OrthographicSize = Amount;

    }


    private void Flip()
    {
        Flipped = !Flipped;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
