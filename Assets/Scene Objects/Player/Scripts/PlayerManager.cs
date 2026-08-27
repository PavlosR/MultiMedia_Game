using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;


    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAnimController animController;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [Header("Stats")]
    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] public float knockBackResist;
    [SerializeField] public float StopTime;
    [SerializeField] public float flightTime;
    [SerializeField] private float iFrameTime;
    [SerializeField] public float parryTime;
    [SerializeField] public bool parrying;
    [SerializeField] private float parryHeal;
    [SerializeField] public bool canParry;
    [SerializeField] public float parryDownTime;
    [SerializeField] private float hitstop;

    public bool iFrames;

    [SerializeField] private AudioClip parryAudio;
    [SerializeField] private AudioClip hurtAudio;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        if (health <= 0f)
        {
            StartCoroutine(die());
        }
    }

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

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        animController = player.GetComponent<PlayerAnimController>();
    }

    public bool Damage(float damage, bool bypass, bool left)
    {
        if (parrying && !bypass)
        {
            ParryLand(left);
            return true;
        }
        else if (!iFrames)
        {
            health -= damage;
            animController.hit();
            StartCoroutine(IFrames());
            impulseSource.GenerateImpulse();
            Hitstop();
            AudioSource.PlayClipAtPoint(hurtAudio, player.transform.position);
            if (!playerController.isGrounded)
            {
                playerController.hitKnock(left);
            }
            return false;
        }
        return false;

    }

    private void ParryLand(bool left)
    {
        parrying = false;
        canParry = true;

        impulseSource.GenerateImpulse();
        AudioSource.PlayClipAtPoint(parryAudio, player.transform.position);

        heal();
        //StartCoroutine(Hitstop());
        StartCoroutine(IFrames());
        if (!playerController.isGrounded)
        {
            playerController.parryKnock(left);
        }
    }

    private void heal()
    {
        health += parryHeal;
    }
    private IEnumerator Hitstop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitstop);
        Time.timeScale = 1f;
    }
    private IEnumerator IFrames()
    {
        iFrames = true;
        yield return new WaitForSeconds(iFrameTime);
        iFrames = false;
    }

    private IEnumerator die()
    {
        Destroy(player);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
