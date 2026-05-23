using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OpeningCutscene : MonoBehaviour
{

    [Header("UI")]

    [SerializeField] private GameObject UIMan;
    [SerializeField] private OpeningUIManager UIManScript;
    [SerializeField] private UISwapInfo UISwapInfo;

    [SerializeField] private float finishCutscene;
    [SerializeField] private bool UIActive;

    [Header("Star")]

    [SerializeField] private GameObject star;
    [SerializeField] private OpeningCutsceneStarSpin starScript;

    [SerializeField] private float startTime;
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
    [SerializeField] private OpeningCutsceneLightPlane lightScript;

    [SerializeField] private float lightPlaneTime;
    [SerializeField] private float lightBrightness;
    [SerializeField] private bool lightVisible;


    [Header("Black Screen")]
    [SerializeField] private GameObject blackScreen;

    [SerializeField] private float fadeOut;
    [SerializeField] private bool blackScreenVisible;

    private float timePassed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        starScript = star.GetComponent<OpeningCutsceneStarSpin>();
        sunScript = sun.GetComponent<OpeningCutsceneSun>();
        lightScript = lightPlane.GetComponent<OpeningCutsceneLightPlane>();
        UIManScript = UIMan.GetComponent<OpeningUIManager>();

        starScript.spinSpeed = starSpinSpeed;

        lightBrightness = 0f;
        lightVisible = false;

        sunVisible = false;

        blackScreenVisible = true;

        timePassed = 0f;

        UIActive = false;
        

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
        lightPlane.SetActive(lightVisible);
        blackScreen.SetActive(blackScreenVisible);

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
        yield return new WaitForSeconds(lightPlaneTime);

        lightVisible = true;
        yield return new WaitForSeconds(fadeOut);

        blackScreenVisible = false;
        sunVisible = false;
        lightScript.fade = true;
        yield return new WaitForSeconds(finishCutscene);

        UIManScript.Swap(UISwapInfo);
    }
}
