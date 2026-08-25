using System.Collections;
using UnityEngine;

public class ThrowPortal : MonoBehaviour
{

    [SerializeField] private Laser laser;
    private Material mat;

    private void Start()
    {
        StartCoroutine("lifeTime");
    }
    private IEnumerator lifeTime()
    {
        Material mat = GetComponent<SpriteRenderer>().material;
        yield return new WaitForSeconds(laser.ChargeTime + laser.TrackTime);
        for (int i = 0; i < 10; i++)
        {
            mat.SetFloat("_Scale", mat.GetFloat("_Scale") + 0.008f);
            yield return new WaitForSeconds(0.025f);
        }
        mat.SetFloat("_Scale", 1.1f);
        if (laser.ShootTime > 0.25)
        {
            yield return new WaitForSeconds(laser.ShootTime);
            for (int i = 0; i < 10; i++)
            {
                mat.SetFloat("_Scale", mat.GetFloat("_Scale") - 0.01f); ;
                yield return new WaitForSeconds(0.025f);
            }
            mat.SetFloat("_Scale", 0f);
        } else
        {
            for (int i = 0; i < 10; i++)
            {
                mat.SetFloat("_Scale", mat.GetFloat("_Scale") - 0.01f); ;
                yield return new WaitForSeconds(0.025f);
            }
            mat.SetFloat("_Scale", 0f);
        }
    }
}
