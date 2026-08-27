using System.Collections;
using UnityEngine;

public class LaserIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private LineRenderer lineRenderer;

    [SerializeField] private Laser.Mode mode;
    [SerializeField] private Laser laser;
    [SerializeField] private float startTime;
    [SerializeField] private float endTime;
    [SerializeField] private float opacity;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        mode = laser.mode;

        StartCoroutine("Countdown");
    }


    private void Update()
    {
        lineRenderer.SetPosition(0, laser.firePoint.position);
        if(mode == Laser.Mode.SetPoint)
        {
            lineRenderer.SetPosition(1, new Vector2(transform.position.x - laser.hitPoint.x, (transform.position.y - laser.hitPoint.y) * -1));
        } 
        else
        {
            lineRenderer.SetPosition(1, laser.hitPoint);
        }

    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine("Scale", true);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(endTime);
        StartCoroutine("Scale", false);
        lineRenderer.enabled = false;
    }

    private IEnumerator Scale(bool In)
    {
        if (In)
        {
            lineRenderer.material.SetFloat("_Opacity", 0);
            for (int i = 0; i < 25; i++)
            {
                lineRenderer.material.SetFloat("_Opacity", lineRenderer.material.GetFloat("_Opacity") + opacity / 25);
                yield return new WaitForSeconds(0.01f);
            }
            lineRenderer.material.SetFloat("_Opacity", opacity);
        } else if (!In)
        {
            lineRenderer.material.SetFloat("_Opacity", opacity);
            for (int i = 0; i < 25; i++)
            {
                lineRenderer.material.SetFloat("_Opacity", lineRenderer.material.GetFloat("_Opacity") - opacity / 25);
                yield return new WaitForSeconds(0.01f);
            }
            lineRenderer.material.SetFloat("_Opacity", 0);
        }
    }
}
