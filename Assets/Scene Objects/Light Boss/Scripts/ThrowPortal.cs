using System.Collections;
using UnityEngine;

public class ThrowPortal : MonoBehaviour
{

    [SerializeField] private Laser laser;

    private void Start()
    {
        StartCoroutine("lifeTime");
    }
    private IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(laser.ChargeTime + laser.TrackTime);
        for (int i = 0; i < 25; i++)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.04f, transform.localScale.y + 0.04f, transform.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        if (laser.ShootTime > 0.25)
        {
            yield return new WaitForSeconds(laser.ShootTime);
            for (int i = 0; i < 25; i++)
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.04f, transform.localScale.y - 0.04f, transform.localScale.z);
                yield return new WaitForSeconds(0.01f);
            }
            transform.localScale = Vector3.zero;    
        } else
        {
            for (int i = 0; i < 25; i++)
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.04f, transform.localScale.y - 0.04f, transform.localScale.z);
                yield return new WaitForSeconds(0.01f);
            }
            transform.localScale = Vector3.zero;
        }
    }
}
