using System.Collections;
using UnityEngine;

public class FloorBeamSpawn : MonoBehaviour
{
    [SerializeField] private int beamCount;
    [SerializeField] private float beamDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject laser;
    [SerializeField] private float beamIntervals;


    private void Start()
    {
        StartCoroutine(beams());
    }
    private IEnumerator beams()
    {

        WaitForSeconds reurn = new WaitForSeconds(beamIntervals);
        for (int i = 0; i < beamCount; i++)
        {
            RaycastHit2D a;
            RaycastHit2D b;
            if (i == 0)
            {
                a = Physics2D.Raycast(new Vector2(transform.position.x, 30), new Vector2(0, -1), 50, layerMask);
                b = Physics2D.Raycast(new Vector2(transform.position.x, 30), new Vector2(0, -1), 50, layerMask); ;
            }
            else
            {
                a = Physics2D.Raycast(new Vector2(transform.position.x + (i * beamDistance), 30), new Vector2(0, -1), 50, layerMask);
                b = Physics2D.Raycast(new Vector2(transform.position.x - (i * beamDistance), 30), new Vector2(0, -1), 50, layerMask);
            }

            Instantiate(laser, a.point, Quaternion.identity);
            if (i != 0)
            {
                Instantiate(laser, b.point, Quaternion.identity);
            }
            yield return reurn;

        }
        Destroy(gameObject);
    }
}
