using UnityEngine;

public class OpeningCutsceneLightPlane : MonoBehaviour
{

    [SerializeField] private float alpha;

    [SerializeField]private SpriteRenderer spriteRenderer;

    public bool fade;
    [SerializeField] private Color newColour;
    [SerializeField] private float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        newColour = spriteRenderer.color;
        alpha = 0f;
        newColour.a = alpha;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (alpha < 1 && !fade) 
        {
            alpha += Time.deltaTime;
            newColour.a = alpha;
            spriteRenderer.color = newColour;
        }

        if ( alpha > 0 && fade)
        {
            alpha -= Time.deltaTime;
            newColour.a = alpha;
            spriteRenderer.color = newColour;
        }
        
    }


}
