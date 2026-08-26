using NUnit.Framework.Constraints;
using UnityEngine;

public class Proj1 : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float damage;

    Rigidbody2D rb;

    [SerializeField] private float RotationControl;

    float MovY, MovX = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Boss").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = transform.position - target.position;
        direction.Normalize();

        float cross = Vector3.Cross(direction, transform.right).z;

        rb.angularVelocity = RotationControl * cross;

        Vector2 vel = transform.right * (MovX * acceleration);
        rb.AddForce(vel);

        float dir = Vector2.Dot(rb.linearVelocity, rb.GetRelativeVector(Vector2.right));
        float thrustForce = Vector2.Dot(rb.linearVelocity, rb.GetRelativeVector(Vector2.down)) * 2;
        Vector2 relForce = Vector2.up * thrustForce;

        rb.AddForce(rb.GetRelativeVector(relForce));

        if(rb.linearVelocity.magnitude > speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        } 
        else if (collision.gameObject == target.gameObject)
        {
            collision.GetComponent<LightBossController>().Health -= damage;
            Destroy(gameObject);

        }
    }
}
