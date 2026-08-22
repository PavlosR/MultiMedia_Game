using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float damage;
    [SerializeField] public bool hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerManager.Damage(damage);
        StartCoroutine("Hit");
    }

    private IEnumerator Hit()
    {
        hit = true;
        yield return new WaitForSeconds(0.05f);
        hit = false;
    }
}
