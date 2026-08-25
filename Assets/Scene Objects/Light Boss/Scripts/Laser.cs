using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Vector3 hitPoint;
    [SerializeField] private Material laserMat;


    private void Awake()
    {
        laserMat = lineRenderer.material;
    }
    private void enableLaser()
    {
        Vector2 Centre = new Vector2(hitPoint.x - firePoint.position.x, hitPoint.y - firePoint.position.y);
        float rotation = Centre.normalized.x * Mathf.Deg2Rad;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hitPoint);
        laserMat.SetVector("Centre", Centre);
        laserMat.SetFloat("Rotation", rotation);
    }

    private void Update()
    {
        Vector2 Centre = new Vector2(hitPoint.x - firePoint.position.x, hitPoint.y - firePoint.position.y);
        float rotation = Mathf.Atan2(Centre.normalized.y, Centre.normalized.x);
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.material.SetVector("_Centre", Centre);
        lineRenderer.material.SetFloat("_Rotation", rotation);

    }

    private void disableLaser()
    {
        lineRenderer.enabled = false;
    }
}
