using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [Header("Stats")]
    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] public float knockBackResist;
    [SerializeField] public float StopTime;
    [SerializeField] public float flightTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }
}
