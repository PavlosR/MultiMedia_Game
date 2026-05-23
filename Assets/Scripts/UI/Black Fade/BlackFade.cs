using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BlackFade : MonoBehaviour
{
    [SerializeField] private float alpha;

    [SerializeField] private Image image;

    public bool fadeDone;

    public bool fade;
    [SerializeField] private Color newColour;
    [SerializeField] private float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        image = GetComponent<Image>();
        fadeDone = false;
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

    public void FadeIn(BlackFadeInfo x = null)
    {
        if (x != null)
        {
            StartCoroutine(FadeInEn(x.length, x.wait));
        }

    }

    public void FadeOut(BlackFadeInfo x = null)
    {
        if (x != null)
        {
            StartCoroutine(FadeOutEn(x.length, x.wait));
        }
    }

    private IEnumerator FadeInEn(float x = 1f, float wait = 0)
    {
        alpha = 0f;
        for (float i = 0; i < x; i += 0.01f)
        {
            alpha += 0.01f / x;
            newColour.a = alpha;
            image.color = newColour;
            yield return new WaitForSeconds(0.01f);
        }

        fadeDone = true;

        if (wait != 0)
        {
            yield return new WaitForSeconds(wait);

            StartCoroutine(FadeOutEn(x));
        }
    }

    private IEnumerator FadeOutEn(float x = 1f, float wait = 0)
    {
        alpha = 1f;
        for (float i = 0; i < x; i += 0.01f)
        {
            alpha -= 0.01f / x;
            newColour.a = alpha;
            image.color = newColour;
            yield return new WaitForSeconds(0.01f);

            fadeDone = false;

            if (wait != 0)
            {
                yield return new WaitForSeconds(wait);

                StartCoroutine(FadeInEn(x));
            }
        }
    }
}
