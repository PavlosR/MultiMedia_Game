using System;
using System.Collections;
using UnityEngine;

public class BigBeamSweapPortal : MonoBehaviour
{
    [SerializeField] private GameObject beamPrefab;
    [SerializeField] public float StartTime;
    [SerializeField] public int attackCount;
    [SerializeField] private SpriteRenderer rend;
    private bool flipped = false;

    private void Start()
    {
        StartCoroutine("Attack");
    }

    private IEnumerator Attack()
    {
        StartCoroutine("Scale", false);
        yield return new WaitForSeconds(StartTime);
        for (int i = 0; i < attackCount; i++)
        {
            GameObject a = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            a.GetComponentInChildren<Laser>().swapSide = flipped;
            flipped = !flipped;
            if (i != attackCount)
            {
                yield return new WaitForSeconds(a.GetComponentInChildren<Laser>().ShootTime + a.GetComponentInChildren<Laser>().ChargeTime);
            }

        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("Scale", true);

    }

    private IEnumerator Scale(bool backwards = false)
    {
        Material mat = rend.material;
        if (!backwards)
        {
            mat.SetFloat("_Scale", 0);
            for (int i = 0; i < 100; i++)
            {
                mat.SetFloat("_Scale", mat.GetFloat("_Scale") + 0.012f);
                yield return new WaitForSeconds(0.015f);
            }
            mat.SetFloat("_Scale", 1.2f);
        }


        if(backwards)
        {
            mat.SetFloat("_Scale", 1.2f);
            for (int i = 0; i < 50; i++)
            {
                mat.SetFloat("_Scale", mat.GetFloat("_Scale") - 0.012f); ;
                yield return new WaitForSeconds(0.03f);
            }
            mat.SetFloat("_Scale", 0f);
            rend.enabled = false;
            Debug.Log($"[{GetInstanceID()}] Scale loop done, calling Destroy at {Time.realtimeSinceStartup}");
            destroy();
        }

    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log($"[{GetInstanceID()}] OnDestroy at {Time.realtimeSinceStartup}");
    }



}
