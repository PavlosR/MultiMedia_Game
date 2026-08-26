using System.Collections;
using UnityEngine;

public class FloorBeamSpawn : MonoBehaviour
{
    [SerializeField] private int beamCount;
    [SerializeField] private float beamDistance;
    [SerializeField] private LayerMask layerMask;


    private IEnumerator beams()
    {
        for (int i = 0; i < beamCount; i++)
        {
            if (i == 0)
            {
                RaycastHit2D a = Physics2D.Raycast(new Vector2(transform.position.x, 30), new Vector2(0, -1), 50, layerMask);
            }
            else
            {
                RaycastHit2D a = Physics2D.Raycast(new Vector2(transform.position.x + (i * beamDistance), 30), new Vector2(0, -1), 50, layerMask);
                RaycastHit2D b = Physics2D.Raycast(new Vector2(transform.position.x - (i * beamDistance), 30), new Vector2(0, -1), 50, layerMask);
            }

        }
        yield return null;
    }
}
