using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{

    [Header("Managers")]
    [SerializeField] private GameObject playerMan;

    public static PlayerManager Instance;

    void Update()
    {
        PlayerManager();
    }

    private void PlayerManager()
    {
        if (Instance == null)
        {
            Instantiate(playerMan);

        }
    }
}
