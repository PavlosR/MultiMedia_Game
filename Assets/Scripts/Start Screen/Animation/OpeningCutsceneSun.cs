using UnityEngine;

public class OpeningCutsceneSun : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public float size;
    public float intensity;

    [SerializeField] private float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        spriteRenderer.material.SetFloat("_Size", spriteRenderer.material.GetFloat("_Size") * size * timePassed);
        spriteRenderer.material.SetFloat("_Intensity", intensity * timePassed);


    }
}
