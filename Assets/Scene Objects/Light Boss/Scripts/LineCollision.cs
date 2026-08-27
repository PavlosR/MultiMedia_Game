using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Laser),typeof(PolygonCollider2D))]
public class LineCollision : MonoBehaviour
{
    List<Vector2> colliderPoints = new List<Vector2>();
    private Laser laser;
    PolygonCollider2D polygonCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laser = GetComponent<Laser>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(laser.firing)
        {
            polygonCollider.enabled = true;
            colliderPoints = CalculateColliderPoints();
            polygonCollider.SetPath(0, colliderPoints);
        } else
        {
            polygonCollider.enabled = false;

        }

    }


    public List<Vector2> CalculateColliderPoints()
    {
        float width = GetComponent<LineRenderer>().startWidth;
        Vector2 hitPoint = transform.InverseTransformPoint(laser.hitPointTrue);
        Vector2 firePoint = transform.InverseTransformPoint(laser.firePoint.position);

        Vector2 dir = (hitPoint - firePoint).normalized;
        Vector2 normal = new Vector2(-dir.y, dir.x) * (width / 4f);

        List<Vector2> colliderPositions = new List<Vector2>
    {
        hitPoint - normal,
        firePoint - normal,
        firePoint + normal,
        hitPoint + normal
    };

        return colliderPositions;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (colliderPoints != null) colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
    }
}
