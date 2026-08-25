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
        Vector2 hitPoint = transform.InverseTransformPoint(laser.hitPoint);
        Vector2 firePoint = transform.InverseTransformPoint(laser.firePoint.position);

        float m = (hitPoint.y - firePoint.y) / (hitPoint.x - firePoint.x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector2[] offsets = new Vector2[2];
        offsets[0] = new Vector2(-deltaX, deltaY);
        offsets[1] = new Vector2(deltaX, -deltaY);

        List<Vector2> colliderPositions = new List<Vector2>
        {
            hitPoint + offsets[0],
            firePoint + offsets[0],
            firePoint + offsets[1],
            hitPoint + offsets[1]


        };

        return colliderPositions;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (colliderPoints != null) colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
    }
}
