using System.Runtime.CompilerServices;
using UnityEngine;

public class openingCutscene : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float lightBrightness;
    [SerializeField] private bool lightVisible;
    [SerializeField] private float sunSize;
    [SerializeField] private bool sunVisible;
    [SerializeField] private float startTime;

    [Header("Star")]
    [SerializeField] private float starFadeTime;
    [SerializeField] private float starSpinSpeed;

    [SerializeField] private GameObject star;
    [SerializeField] private openingCutsceneStarSpin starScript;

    [Header("GameObjects")]

    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject blackScreen;

    private float timePassed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        starScript = star.GetComponent<openingCutsceneStarSpin>();
        starScript.spinSpeed = starSpinSpeed;
        lightBrightness = 0f;
        lightVisible = false;
        sunSize = 0f;
        sunVisible = false;
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= startTime) 
        { 
            star.SetActive(true);
        }

        if (timePassed >= startTime + starFadeTime)
        {
            star.SetActive(false);
        }
    }
}
