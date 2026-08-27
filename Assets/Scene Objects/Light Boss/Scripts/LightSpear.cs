using System.Collections;
using UnityEngine;

public class LightSpear : MonoBehaviour
{
    [SerializeField] private float deathTime;
    [SerializeField] GameObject floorbeam;
    private void Start()
    {
        StartCoroutine("DeathTime");
    }
    private void LateUpdate()
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

    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(GetComponent<Collide>().collided)
        {
            Instantiate(floorbeam, transform.position, Quaternion.identity);
        }

    }
}
