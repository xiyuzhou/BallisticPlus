using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : Seek
{
    public List<GameObject> path;

    int currentPathIndex;
    float targetRadius = 1f;

    public override SteeringOutput getSteering()
    {
        if (target == null)
        {
            float nearestDist = float.PositiveInfinity;
            int nearestIndex = 0;
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 diff = path[i].transform.position - character.transform.position;
                float dist = diff.magnitude;
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestIndex = i;
                }
            }
            target = path[nearestIndex];
        }
        else
        {
            float distToTarget = (target.transform.position - character.transform.position).magnitude;
            if (distToTarget < targetRadius)
            {
                currentPathIndex++;
                currentPathIndex %= path.Count;
                target = path[currentPathIndex];
            }
        }
        return base.getSteering();
    }
}
