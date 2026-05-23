using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningUIManager : MonoBehaviour
{

    [SerializeField] private GameObject blackFade;
    [SerializeField] private BlackFade blackFadeScript;

    [Header("UI Canvas'")]

    [SerializeField] private GameObject openingMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject playMenu;

    private void Awake()
    {
        blackFadeScript = blackFade.GetComponent<BlackFade>();
    }



    public void Swap(UISwapInfo swapScript)
    {
        if (swapScript.fade == true)
        {
            StartCoroutine(SwapEn(swapScript.current, swapScript.enable));
        } 
        else
        {
            if (swapScript.current != null)
            {
                swapScript.current.SetActive(false);
            }

            swapScript.enable.SetActive(true);
        }

    }

    public void SceneLoad(UISwapInfo swapScript)
    {
        if (swapScript.fade  == true)
        {
            StartCoroutine(SceneLoadEn(swapScript.Scene));
        }
        else
        {
            SceneManager.LoadScene(swapScript.Scene);
        }


    }

    private IEnumerator SwapEn(GameObject current, GameObject enable)
    {

        yield return new WaitUntil(() => blackFadeScript.fadeDone);

        if (current != null)
        {
            current.SetActive(false);
        }

        enable.SetActive(true);

    }

    private IEnumerator SceneLoadEn(string scene)
    {

        yield return new WaitUntil(() => blackFadeScript.fadeDone);

        SceneManager.LoadScene(scene);

    }
}
