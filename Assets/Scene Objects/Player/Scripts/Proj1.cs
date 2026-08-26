using UnityEngine;

public class Proj1 : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    Rigidbody2D rb;

    [SerializeField] private float RotationControl;

    float MovY, MovX = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       rb = GetComponent<Rigidbody2D>();     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
    }
}
