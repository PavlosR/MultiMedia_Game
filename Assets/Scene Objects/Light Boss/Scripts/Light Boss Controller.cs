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
    [Header("Walk Variables")]
    [SerializeField] private float xDistance;
    [SerializeField] private float walkSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<LightBossAnimController>();
    }
    void Start()
    {
        //StartCoroutine("Walk", 1);
        animController.Attack1();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.x - transform.position.x > 0 && Flipped)
        {
            Flip();
        } else if (Player.transform.position.x - transform.position.x < 0 && !Flipped)
        {
            Flip();
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

        yield return null;
    }

    private void Flip()
    {
        Flipped = !Flipped;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
