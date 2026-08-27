using System.Collections;
using UnityEngine;

public class Collide : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float damage;
    [SerializeField] public bool hit;
    [SerializeField] public bool collided;
    [SerializeField] public bool destroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide");
        if (collision.gameObject.TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag("Collidable"))
            {
                StartCoroutine("collide");
            }

        }
    }

    private IEnumerator collide()
    {
        collided = true;
        hit = true;
        yield return new WaitForSeconds(0.05f);
        hit = false;
        if (destroy)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
