using System.Collections;
using UnityEngine;

public class LightBossAnimController : MonoBehaviour
{

    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject smear;

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
        SetAction(3);
        NextAction();
        yield return new WaitForSeconds(0.5f);
        SetAction(0);

        NextAction();
        yield return new WaitForSeconds(0.75f);

        NextAction();
        yield return new WaitForSeconds(0.85f);

        NextAction();
        yield return new WaitForSeconds(1.2f);

        NextAction();
        yield return null;
    }


}
