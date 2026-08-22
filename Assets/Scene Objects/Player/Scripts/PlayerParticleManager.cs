using System.Collections;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem absorb;
    [SerializeField] private ParticleSystem release;
    [SerializeField] private GameObject ImpactRelease;


    public void Impact(float time)
    {
        StartCoroutine("ImpactCor", time);
    }

    private IEnumerator ImpactCor(float time)
    {
        absorb.gameObject.SetActive(true);
        absorb.Play();
        yield return new WaitForSeconds(time);
        release.Play();
        release.gameObject.SetActive(true);
        ImpactRelease.SetActive(true);
    }
}
