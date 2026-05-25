using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Input Manager")]
    [SerializeField] private InputManagerPlayer inputMan;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        Debug.Log("Jump");
        rb.AddForce(new Vector3(0, 10, 0));
    }
}
