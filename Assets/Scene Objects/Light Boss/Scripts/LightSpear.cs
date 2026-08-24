using UnityEngine;

public class LightSpear : MonoBehaviour
{

    private void Update()
    {
        float angle = Mathf.Atan2(-GetComponent<Rigidbody2D>().linearVelocityX, GetComponent<Rigidbody2D>().linearVelocityY) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void FixedUpdate()
    {
        if (GetComponent<Damage>().hit)
        {
            GetComponent<Damage>().hit = false;

        }
    }
}
