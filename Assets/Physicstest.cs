using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physicstest : MonoBehaviour
{
    public float radius = 3f; // show penetration into the colliders located inside a sphere of this radius
    public int maxNeighbours = 16; // maximum amount of neighbours visualised

    private Collider[] neighbours;

    public void Start()
    {
        neighbours = new Collider[maxNeighbours];
    }

    public void OnDrawGizmos()
    {
        var thisCollider = GetComponent<Collider>();

        if (!thisCollider)
            return; // nothing to do without a Collider attached

        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, neighbours,1<<6);

        for (int i = 0; i < count; ++i)
        {
            var collider = neighbours[i];

            if (collider == thisCollider)
                continue; // skip ourself

           

            Vector3 otherPosition = collider.gameObject.transform.position;
            Quaternion otherRotation = collider.gameObject.transform.rotation;

            Vector3 direction;
            float distance;

            bool overlapped = Physics.ComputePenetration(
                thisCollider, transform.position, transform.rotation,
                collider, otherPosition, otherRotation,
                out direction, out distance
            );

            // draw a line showing the depenetration direction if overlapped
            if (overlapped)
            {
               
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(otherPosition, direction * distance);
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position+(direction*distance),1f);
            }
        }
    }
}
