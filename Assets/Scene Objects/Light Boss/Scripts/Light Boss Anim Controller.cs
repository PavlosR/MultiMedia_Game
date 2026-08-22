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

    public void Attack1()
    {
        StartCoroutine("Attack1Cor");

    }

    private IEnumerator Attack1Cor()
    {
        swordRend.material.SetFloat("_Scan_Scale", 1f);
        SetAction(3);
        NextAction();
        StartCoroutine("SwordSpawn");
        yield return new WaitForSeconds(0.5f);


        SetAction(0);

        NextAction();
        yield return new WaitForSeconds(0.75f);

        NextAction();
        yield return new WaitForSeconds(0.85f);

        NextAction();
        yield return new WaitForSeconds(1.2f);

        NextAction();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("OutlineSpawn");
    }

    private IEnumerator SwordSpawn()
    {
        for (int i = 0; i < 60; i++)
        {
            swordRend.material.SetFloat("_Scan_Scale", swordRend.material.GetFloat("_Scan_Scale") - 0.025f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator OutlineSpawn()
    {
        outlineRend.material.SetFloat("_Scan_Scale", 0.25f);
        for (int i = 0; i < 20; i++)
        {
            outlineRend.material.SetFloat("_Scan_Scale", outlineRend.material.GetFloat("_Scan_Scale") - 0.05f);
            yield return new WaitForSeconds(0.02f);
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
        bodyRend.material.SetFloat("_Strength", 1);
        armRend.material.SetFloat("_Strength", 1);
        for (int i = 0;i < 25;i++)
        {
            bodyRend.material.SetFloat("_Scale", bodyRend.material.GetFloat("_Scale") + 0.006f);
            armRend.material.SetFloat("_Scale", armRend.material.GetFloat("_Scale") + 0.006f);

            yield return new WaitForSeconds(0.005f);
        }


        bodyRend.material.SetFloat("_Scale", 0.65f);
        armRend.material.SetFloat("_Scale", 0.65f);
    }

    private IEnumerator TeleportReverseCor()
    {
        for (int i = 0; i < 25; i++)
        {
            bodyRend.material.SetFloat("_Scale", bodyRend.material.GetFloat("_Scale") - 0.006f);
            armRend.material.SetFloat("_Scale", armRend.material.GetFloat("_Scale") - 0.006f);

            yield return new WaitForSeconds(0.005f);
        }

        bodyRend.material.SetFloat("_Strength", 0);
        armRend.material.SetFloat("_Strength", 0);

        bodyRend.material.SetFloat("_Scale", 0.50038f);
        armRend.material.SetFloat("_Scale", 0.50038f);
    }
}
