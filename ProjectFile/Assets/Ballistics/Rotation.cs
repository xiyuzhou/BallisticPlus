using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 targetAngle;
    public float timet = 5.0f;

    public void AngleUpdate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetAngle);
        transform.rotation = targetRotation;
    }


}
