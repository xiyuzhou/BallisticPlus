using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleAvoidance : Seek
{
    public float avoidDistance = 10f;

    public float lookahead = 5f;

    protected override Vector3 getTargetPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead))
        {
            return hit.point - (hit.normal * avoidDistance);
        }
        else
        {
            return base.getTargetPosition();
        }
    }
}