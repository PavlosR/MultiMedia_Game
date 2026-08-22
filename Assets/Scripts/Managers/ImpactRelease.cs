using UnityEngine;

public class ImpactRelease : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private GameObject Player;


    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 1);
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);
    }

    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.03f, transform.localScale.x + 0.03f, 1);
        if (transform.localScale.x >= 100f)
        {
            gameObject.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        if (transform.localScale.x > 0)
        {
            rend.material.SetFloat("_Opacity", 6 / transform.localScale.x - 0.02f);
        }
        else
        {
            rend.material.SetFloat("_Opacity", 100);
        }
    }
}
