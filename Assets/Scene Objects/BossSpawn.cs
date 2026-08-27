using System.Collections;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{


    [SerializeField] private float SpawnTime;
    [SerializeField] private GameObject boss;
    [SerializeField] private PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(bossSpawn()); 
    }

    private IEnumerator bossSpawn()
    {
        yield return new WaitForSeconds(SpawnTime);
        boss.SetActive(true);
        player.attackCooldown = false;
    }
}
