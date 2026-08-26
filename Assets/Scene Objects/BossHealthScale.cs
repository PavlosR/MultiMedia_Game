using UnityEngine;
using UnityEngine.UI;

public class BossHealthScale : MonoBehaviour
{
    [SerializeField] private Image rend;
    [SerializeField] private GameObject boss;
    private LightBossController bossController;


    private void Awake()
    {
        rend = GetComponent<Image>();
        bossController = boss.GetComponent<LightBossController>();
    }
    void Update()
    {
        rend.material.SetFloat("_Scale", bossController.Health);
    }
}
