using System.Collections;
using UnityEngine;

public class LightBossAnimController : MonoBehaviour
{

    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject smear;
    [SerializeField] private GameObject outline;

    [SerializeField] private SpriteRenderer swordRend;
    [SerializeField] private SpriteRenderer armRend;
    [SerializeField] private SpriteRenderer bodyRend;
    [SerializeField] private SpriteRenderer outlineRend;

    private Animator armAn;
    private Animator bodyAn;
    private Animator swordAn;
    private Animator smearAn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        armAn = arm.GetComponent<Animator>();
        bodyAn = body.GetComponent<Animator>();
        swordAn = sword.GetComponent<Animator>();
        smearAn = smear.GetComponent<Animator>();

        swordRend = sword.GetComponent<SpriteRenderer>();
        bodyRend = body.GetComponent<SpriteRenderer>();
        armRend = arm.GetComponent<SpriteRenderer>();
        outlineRend = outline.GetComponent<SpriteRenderer>();

    }
    
    private void SetAction(int i)
    {
        armAn.SetInteger("Action Num", i);
        bodyAn.SetInteger("Action Num", i);
        swordAn.SetInteger("Action Num", i);
        smearAn.SetInteger("Action Num", i);
    }

    private void NextAction()
    {
        armAn.SetTrigger("Continue Action");
        bodyAn.SetTrigger("Continue Action");
        swordAn.SetTrigger("Continue Action");
        smearAn.SetTrigger("Continue Action");
    }

    public void Idle()
    {
        SetAction(1);
    }

    public void Walk()
    {
        SetAction(2);
        NextAction();
    }
    public void Attack4()
    {
        SetAction(6);
        NextAction();
    }

    public void Attack3()
    {
        SetAction(5);
        NextAction();
    }
    public void Attack2()
    {
        StartCoroutine("Attack2Cor");
    }
    public void Attack1(int count)
    {
        StartCoroutine("Attack1Cor", count);

    }

    private IEnumerator Attack1Cor(int count)
    {
        swordRend.material.SetFloat("_Scan_Scale", 1f);
        SetAction(3);
        NextAction();
        StartCoroutine("SwordSpawn");
        yield return new WaitForSeconds(0.5f);
        if (count >= 1)
        {



            SetAction(0);
            NextAction();
            yield return new WaitForSeconds(0.75f);


            if (count >= 2)
            {


                NextAction();
                yield return new WaitForSeconds(0.85f);

                if (count >= 3)
                {
                    NextAction();
                    yield return new WaitForSeconds(1.2f);

                    if (count >= 4)
                    {
                        NextAction();
                        StartCoroutine("OutlineSpawn");
                    }

                }


            }

        }

    }

    private IEnumerator Attack2Cor()
    {

        SetAction(4);
        NextAction();
        StartCoroutine("SwordSpawn");
        yield return new WaitForSeconds(0.5f);


        SetAction(0);

        NextAction();
        yield return new WaitForSeconds(0.7f);
    }


    private IEnumerator SwordSpawn()
    {
        swordRend.material.SetFloat("_Scan_Scale", 1f);
        WaitForSeconds _delay = new WaitForSeconds(0.01f);
        for (int i = 0; i < 60; i++)
        {
            swordRend.material.SetFloat("_Scan_Scale", swordRend.material.GetFloat("_Scan_Scale") - 0.025f);
            yield return _delay;
        }
    }


    private IEnumerator OutlineSpawn()
    {
        outlineRend.material.SetFloat("_Scan_Scale", 0.25f);
        WaitForSeconds _delay = new WaitForSeconds(0.02f);
        for (int i = 0; i < 40; i++)
        {
            outlineRend.material.SetFloat("_Scan_Scale", outlineRend.material.GetFloat("_Scan_Scale") - 0.025f);
            yield return _delay;
        }
        outlineRend.material.SetFloat("_Scan_Scale", -0.75f);
    }

    public void Teleport()
    {
        StartCoroutine("TeleportCor");
    }

    public void TeleportReverse()
    {
        StartCoroutine("TeleportReverseCor");
    }

    private IEnumerator TeleportCor()
    {
        bodyRend.material.SetFloat("_Scale", 0.50038f);
        armRend.material.SetFloat("_Scale", 0.50038f);
        bodyRend.material.SetFloat("_Strength", 1);
        armRend.material.SetFloat("_Strength", 1);
        WaitForSeconds _delay = new WaitForSeconds(0.005f);
        for (int i = 0;i < 25;i++)
        {
            bodyRend.material.SetFloat("_Scale", bodyRend.material.GetFloat("_Scale") + 0.006f);
            armRend.material.SetFloat("_Scale", armRend.material.GetFloat("_Scale") + 0.006f);

            yield return _delay;
        }


        bodyRend.material.SetFloat("_Scale", 0.65f);
        armRend.material.SetFloat("_Scale", 0.65f);
    }

    private IEnumerator TeleportReverseCor()
    {
        bodyRend.material.SetFloat("_Scale", 0.65f);
        armRend.material.SetFloat("_Scale", 0.65f);
        WaitForSeconds _delay = new WaitForSeconds(0.005f);
        for (int i = 0; i < 25; i++)
        {
            bodyRend.material.SetFloat("_Scale", bodyRend.material.GetFloat("_Scale") - 0.006f);
            armRend.material.SetFloat("_Scale", armRend.material.GetFloat("_Scale") - 0.006f);

            yield return _delay;
        }

        bodyRend.material.SetFloat("_Strength", 0);
        armRend.material.SetFloat("_Strength", 0);

        bodyRend.material.SetFloat("_Scale", 0.50038f);
        armRend.material.SetFloat("_Scale", 0.50038f);
    }
}
