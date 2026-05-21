using UnityEngine;

public class OpeningCutsceneSun : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public float size;
    public float intensity;
    public Vector2 scale;

    [SerializeField] private float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        timePassed = 0f;
        scale = Vector2.one;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        scale = new Vector2(scale.x + Time.deltaTime * 5, scale.y + Time.deltaTime * 5);
        gameObject.transform.localScale = new Vector3(scale.x, scale.y, 1);

        spriteRenderer.material.SetFloat("_Size", size * timePassed);
        spriteRenderer.material.SetFloat("_Intensity", intensity * timePassed);


    }
}
