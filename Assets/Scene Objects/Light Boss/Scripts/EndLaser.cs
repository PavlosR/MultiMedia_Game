using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EndLaser : MonoBehaviour
{
    public float time;
    public Laser laser;
    private bool changeSize = false;
    private void Start()
    {
        StartCoroutine("LifeTime", time);
    }

    private void Update()
    {
        if (laser != null)
        {
            transform.position = laser.hitPoint;
        }

        if (changeSize)
        {
            var mainModule = GetComponent<ParticleSystem>().main;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSizeXMultiplier / 1.1f * Time.deltaTime, mainModule.startSizeXMultiplier / 1.1f * Time.deltaTime);
        }
    }

    private IEnumerator LifeTime(float time)
    {
        yield return new WaitForSeconds(time);
        changeSize = true;

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
