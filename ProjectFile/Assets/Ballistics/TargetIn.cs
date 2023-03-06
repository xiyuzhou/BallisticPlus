using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject canon = GameObject.Find("Canon");
        if (canon != null)
        {
            //canon.GetComponent<Canon>().target = this.transform;
            //canon.GetComponent<Canon>().CalculateForce();
        }
    }
}
