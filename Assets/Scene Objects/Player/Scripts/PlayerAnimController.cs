using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;



public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject legs;
    [SerializeField] private GameObject orb;

    [SerializeField] private SpriteRenderer armRend;
    [SerializeField] private SpriteRenderer bodyRend;
    [SerializeField] private SpriteRenderer legsRend;
    [SerializeField] private SpriteRenderer orbRend;

    [SerializeField] private Material hitMat;
    [SerializeField] private Material bodyMat;
    [SerializeField] private Material armsMat;
    [SerializeField] private Material legsMat;
    [SerializeField] private Material orbMat;
    [SerializeField] private Material parryMat;
    private bool parryMatChange;
    

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerManager playerManager;

    private Animator armAn;
    private Animator bodyAn;
    private Animator legsAn;
    private Animator orbAn;

    private void Awake()
    {
        playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
        armAn = arm.GetComponent<Animator>();
        bodyAn = body.GetComponent<Animator>();
        legsAn = legs.GetComponent<Animator>();
        orbAn = orb.GetComponent<Animator>();

        armRend = arm.GetComponent<SpriteRenderer>();
        bodyRend = body.GetComponent<SpriteRenderer>();
        legsRend = legs.GetComponent<SpriteRenderer>();
        orbRend = orb.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        armsMat = armRend.material;
        bodyMat = bodyRend.material;
        legsMat = legsRend.material;
        orbMat = orbRend.material;

    }

    private void Update()
    {
        if (playerManager.parrying)
        {
            SetAction(2);



        }
        else if (playerController.attacking)
        {
            armAn.SetInteger("Action Num", 3);
            bodyAn.SetInteger("Action Num", 3);
            legsAn.SetInteger("Action Num", 3);
            orbAn.SetInteger("Action Num", 3);
        }
        else if (!playerController.isGrounded)
        {
            if (playerController.rb.linearVelocityY > 0)
            {
                SetAction(4);
            } else if (playerController.rb.linearVelocityY < 0)
            {
                SetAction(5);
            }
        } else if (playerController.rb.linearVelocityX != 0)
        {
            SetAction(1);
        } else
        {
            SetAction(0);
        }
    }
    private void SetAction(int i)
    {
        if (playerManager.parrying && !parryMatChange)
        {
            armRend.material = parryMat;
            bodyRend.material = parryMat;
            legsRend.material = parryMat;
            orbRend.material = parryMat;
            parryMatChange = true;
        }  
        else if (!playerManager.parrying && parryMatChange)
        {
            armRend.material = armsMat;
            bodyRend.material = bodyMat;
            legsRend.material = legsMat;
            orbRend.material = orbMat;
            parryMatChange = false;
        }
        armAn.SetInteger("Action Num", i);
        bodyAn.SetInteger("Action Num", i);
        legsAn.SetInteger("Action Num", i);
        orbAn.SetInteger("Action Num", i);
    }

    public void hit()
    {
        StartCoroutine(hitCor());
    }

    private IEnumerator hitCor()
    {
        Material armOgMat = armRend.material;
        Material bodyOgMat = bodyRend.material;
        Material legsOgMat = legsRend.material;
        Material orbOgMat = orbRend.material;

        armRend.material = hitMat;
        bodyRend.material = hitMat;
        legsRend.material = hitMat;
        orbRend.material = hitMat;

        for (int i = 0; i < 10; i++)
        {
            armRend.material.SetFloat("_Opacity", armRend.material.GetFloat("_Opacity") + 0.05f);
            bodyRend.material.SetFloat("_Opacity", bodyRend.material.GetFloat("_Opacity") + 0.05f);
            legsRend.material.SetFloat("_Opacity", legsRend.material.GetFloat("_Opacity") + 0.05f);
            orbRend.material.SetFloat("_Opacity", orbRend.material.GetFloat("_Opacity") + 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        armRend.material.SetFloat("_Opacity", 0.5f);
        bodyRend.material.SetFloat("_Opacity", 0.5f);
        legsRend.material.SetFloat("_Opacity", 0.5f);
        orbRend.material.SetFloat("_Opacity", 0.5f);
        for (int i = 0; i < 10; i++)
        {
            armRend.material.SetFloat("_Opacity", armRend.material.GetFloat("_Opacity") - 0.05f);
            bodyRend.material.SetFloat("_Opacity", bodyRend.material.GetFloat("_Opacity") - 0.05f);
            legsRend.material.SetFloat("_Opacity", legsRend.material.GetFloat("_Opacity") - 0.05f);
            orbRend.material.SetFloat("_Opacity", orbRend.material.GetFloat("_Opacity") - 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        armRend.material.SetFloat("_Opacity", 0f);
        bodyRend.material.SetFloat("_Opacity", 0f);
        legsRend.material.SetFloat("_Opacity", 0f);
        orbRend.material.SetFloat("_Opacity", 0f);

        armRend.material = armOgMat;
        bodyRend.material = bodyOgMat;
        legsRend.material = legsOgMat;
        orbRend.material = orbOgMat;
    }


}
