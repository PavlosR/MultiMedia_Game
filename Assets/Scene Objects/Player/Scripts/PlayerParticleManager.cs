using System.Collections;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem absorb;
    [SerializeField] private ParticleSystem release;
    [SerializeField] private ParticleSystem star;
    [SerializeField] private GameObject starObj;
    [SerializeField] private GameObject ImpactRelease;
    [SerializeField] private GameObject Player;


    public void Impact(float time)
    {
        StartCoroutine("ImpactCor", time);
    }

    private IEnumerator ImpactCor(float time)
    {
        
        var emissionModule = star.emission;
        absorb.gameObject.SetActive(true);
        absorb.Play();
        emissionModule.rateOverTime = (1 / time) * 6;
        StartCoroutine("StarShape", time);

        yield return new WaitForSeconds(time);
        release.Play();
        release.gameObject.SetActive(true);
        GameObject a = Instantiate(ImpactRelease);
        a.transform.position = Player.transform.position;
    }

    private IEnumerator StarShape(float time)
    {
        starObj.SetActive(true);
        star.Play();
        starObj.transform.localPosition = new Vector3(0, 2, 0).normalized * 2;
        yield return new WaitForSeconds(time / 5);
        starObj.transform.localPosition = new Vector3(1, -2, 0).normalized * 2;
        yield return new WaitForSeconds(time / 5);
        starObj.transform.localPosition = new Vector3(-2, 1, 0).normalized * 2;
        yield return new WaitForSeconds(time / 5);
        starObj.transform.localPosition = new Vector3(2, 1, 0).normalized * 2;
        yield return new WaitForSeconds(time / 5);
        starObj.transform.localPosition = new Vector3(-1, -2, 0).normalized * 2;
        yield return new WaitForSeconds(time / 5);
        starObj.transform.localPosition = new Vector3(0, 2, 0).normalized * 2;
        yield return new WaitForSeconds(time / 2);
        starObj.SetActive(false);
    }



}
