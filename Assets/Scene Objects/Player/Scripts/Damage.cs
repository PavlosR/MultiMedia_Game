using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float damage;
    [SerializeField] public bool hit;
    [SerializeField] public bool destroy;
    [SerializeField] public bool bypassParry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Tags>(out var tags)) {
            if(tags.HasTag("Player"))
            {
                float direction = collision.transform.position.x - transform.position.x;
                Debug.Log(direction);
                bool left;
                if (direction < 0)
                {
                    left = true;
                }
                else
                {
                    left = false;
                }
                playerManager.Damage(damage, bypassParry, left);
                if (!playerManager.iFrames)
                {
                    StartCoroutine("Hit");
                }

            }

        }

    }

    private IEnumerator Hit()
    {

        hit = true;
        yield return new WaitForSeconds(0.05f);
        hit = false;
        if(destroy)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
