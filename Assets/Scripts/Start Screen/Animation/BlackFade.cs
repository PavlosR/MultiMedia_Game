using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BlackFade : MonoBehaviour
{
    [SerializeField] private float alpha;

    [SerializeField] private Image image;

    public bool fade;
    [SerializeField] private Color newColour;
    [SerializeField] private float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        newColour = image.color;
        alpha = 0f;
        newColour.a = alpha;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void FadeIn(float x = 1f)
    {
        while (timePassed < x)
        {
            timePassed += Time.fixedDeltaTime;
            alpha += Time.fixedDeltaTime / x;
            newColour.a = alpha;
            image.color = newColour;

        }
    }
    public void FadeOut(float x = 1f)
    {
        while (timePassed < x)
        {
            timePassed += Time.fixedDeltaTime;
            alpha -= Time.fixedDeltaTime / x;
            newColour.a = alpha;
            image.color = newColour;

        }
    }
}
