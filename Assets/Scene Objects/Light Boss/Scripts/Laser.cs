using System;
using System.Collections;
using System.Drawing;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private LineCollision lineCollision;
    public Transform firePoint;
    public Vector3 hitPoint;
    private Material laserMat;
    [SerializeField] LayerMask targetLayers;
    public bool particleSpawned = false;

    public bool firing;

    public enum Mode { Track, SetPoint, Moving, Target, None }
    [SerializeField] public Mode mode;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject endParticle;
    [SerializeField] public float ChargeTime;
    [SerializeField] public float ShootTime;
    [SerializeField] public float TrackTime;

    [SerializeField] float distance = 10f;



    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        laserMat = lineRenderer.material;
        lineCollision = GetComponent<LineCollision>();

        Player = GameObject.Find("Player");
        hitPoint = Player.transform.position;
    }

    private void Start()
    {
        StartCoroutine("laserCount");
    }
    private void enableLaser()
    {

        Vector2 Centre = new Vector2(hitPoint.x - firePoint.position.x, hitPoint.y - firePoint.position.y);
        float rotation = Mathf.Atan2(Centre.normalized.y, Centre.normalized.x);

        lineRenderer.enabled = true;
        lineCollision.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.material.SetVector("_Centre", Centre);
        lineRenderer.material.SetFloat("_Rotation", rotation);
        lineRenderer.material.SetFloat("_Scale", 57 * MathF.Pow(0.875f, lineRenderer.startWidth));
        lineRenderer.material.SetFloat("_Speed", -1.67f * MathF.Pow(1.195f, lineRenderer.startWidth));

        if(!particleSpawned)
        {
            particleSpawned = true;
            GameObject a = Instantiate(endParticle, hitPoint, Quaternion.identity);
            a.GetComponent<EndLaser>().time = ShootTime + 1f;
            a.GetComponent<EndLaser>().laser = this;
            var mainModule = a.GetComponent<ParticleSystem>().main;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(1.0f * lineRenderer.startWidth, 5.0f * lineRenderer.startWidth);
        }


    }

    private void Update()
    {
        if (mode == Mode.Track)
        {
            Vector2 point = Player.transform.position;
            Vector2 origin = firePoint.position;
            Vector2 direction = new Vector2(point.x - origin.x, point.y - origin.y).normalized;
            Track(point, origin, direction);
            Vector2 hit = Raycast(origin, direction);

            if (float.IsNaN(hit.x) || float.IsNaN(hit.y))
            {
                hitPoint = origin + (direction * distance);
            } else
            {
                hitPoint = new Vector2(hit.x + direction.x, hit.y + direction.y);
            }
        }



    }

    private IEnumerator disableLaser()
    {
        StartCoroutine("widthChange", false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void Track(Vector2 point, Vector2 origin, Vector2 direction)
    {

        Vector2 hit = Raycast(origin, direction);

        if (float.IsNaN(hit.x) || float.IsNaN(hit.y))
        {
            // Math: Origin + (Direction * Distance)
            point = origin + (direction * distance);
            Debug.DrawLine(origin, point, UnityEngine.Color.red);
        }
        else
        {
            Debug.DrawLine(origin, hit, UnityEngine.Color.green);
        }
    }

    private Vector2 Raycast(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, targetLayers);


        if (hit.collider == null)
        {
            return new Vector2(float.NaN, float.NaN);
        }
        return hit.point;
    }

    private IEnumerator laserCount()
    {
        yield return new WaitForSeconds(TrackTime);
        mode = Mode.None;
        yield return new WaitForSeconds(ChargeTime);
        enableLaser();
        firing = true;
        StartCoroutine("widthChange", true);
        yield return new WaitForSeconds(ShootTime);
        firing = false;
        StartCoroutine("disableLaser");
    }

    private IEnumerator widthChange(bool enable)
    {
        if (enable)
        {
            lineRenderer.material.SetFloat("_Width", -0.1f);
            for (int i = 0; i < 25; i++)
            {
                lineRenderer.material.SetFloat("_Width", lineRenderer.material.GetFloat("_Width") + 0.04f);
                yield return new WaitForSeconds(0.01f);
            }
            lineRenderer.material.SetFloat("_Width", 1);
        } else
        {
            lineRenderer.material.SetFloat("_Width", 1);
            for (int i = 0; i < 25; i++)
            {
                lineRenderer.material.SetFloat("_Width", lineRenderer.material.GetFloat("_Width") - 0.04f);
                yield return new WaitForSeconds(0.01f);
            }
            lineRenderer.material.SetFloat("_Width", -0.1f);
        }

    }
}
