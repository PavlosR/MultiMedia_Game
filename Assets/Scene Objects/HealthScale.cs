
using UnityEngine;
using UnityEngine.UI;

public class HealthScale : MonoBehaviour
{
    [SerializeField] private Image rend;
    [SerializeField] private GameObject player;
    private PlayerManager playerManager;


    private void Awake()
    {
        rend = GetComponent<Image>();
        playerManager = player.GetComponent<PlayerManager>();
    }
    void Update()
    {
        rend.material.SetFloat("_Scale", playerManager.health);
    }
}
