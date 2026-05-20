using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class openingCutscene : MonoBehaviour
{ 
    [SerializeField] private float startTime;

    [SerializeField] private GameObject blackScreen;

    [Header("Star")]

    [SerializeField] private GameObject star;
    [SerializeField] private OpeningCutsceneStarSpin starScript;

    [SerializeField] private float starFadeTime;
    [SerializeField] private float starSpinSpeed;
    [SerializeField] private bool starVisible;


    [Header("Sun")]

    [SerializeField] private GameObject sun;
    [SerializeField] private OpeningCutsceneSun sunScript;

    [SerializeField] private float sunTime;
    [SerializeField] private float sunSize;
    [SerializeField] private float sunIntensity;
    [SerializeField] private bool sunVisible;



    [Header("Light")]

    [SerializeField] private GameObject lightPlane;

    [SerializeField] private float lightBrightness;
    [SerializeField] private bool lightVisible;

    private float timePassed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        starScript = star.GetComponent<OpeningCutsceneStarSpin>();
        sunScript = sun.GetComponent<OpeningCutsceneSun>();

        starScript.spinSpeed = starSpinSpeed;

        lightBrightness = 0f;
        lightVisible = false;

        sunVisible = false;

        timePassed = 0f;
        

    }

    private void Start()
    {
        StartCoroutine("CutsceneStart");
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        star.SetActive(starVisible);
        sun.SetActive(sunVisible);

        sunScript.size = sunSize;
        sunScript.intensity = sunIntensity;




    }

    private IEnumerator CutsceneStart()
    {
        yield return new WaitForSeconds(startTime);

        starVisible = true;
        yield return new WaitForSeconds(starFadeTime);

        starVisible = false;
        yield return new WaitForSeconds(sunTime);
        sunVisible = true;

        yield return null;
    }
}
