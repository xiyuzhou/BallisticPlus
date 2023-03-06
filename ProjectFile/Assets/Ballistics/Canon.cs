using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Canon : MonoBehaviour
{
    public Transform target;
    public float myTimeScale = 1.0f;
    public float LaunchForce = 1f;
    public TextMeshProUGUI textComponent2;
    public GameObject CanonBarrel;
    public float CurrentForce;
    public GameObject bulletPrefab;
    public GameObject cameraMan;
    public Transform SpawnPos;
    Rigidbody rb;
    Vector3 startPos;
    GameObject lastSpawnedObject = null;


    public void updateForce(float a)
    {
        CurrentForce = a;
        CalculateAngle(CurrentForce);
    }
    void Start()
    {
        startPos = transform.position;
        Time.timeScale = myTimeScale; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();
        textComponent2.text = "Please select a target first(press M)";
    }
    
    public void UpdateRText(float value)
    {
        Debug.Log("1");
        textComponent2.text = "Required Force: " + value;
    }

    public float CalculateForce()
    {
        LaunchForce = 1;
        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = fs.Calculate(transform.position, target.position, LaunchForce, Physics.gravity);
        while (!aimVector.HasValue)
        {
            LaunchForce += 2f;
            aimVector = fs.Calculate(transform.position, target.position, LaunchForce, Physics.gravity);
        }
        Debug.Log(LaunchForce);
        Debug.Log(aimVector.Value.normalized);
        UpdateRText(LaunchForce);
        return LaunchForce;
    }
    public void CalculateAngle(float Currentforce)
    {
        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = fs.Calculate(transform.position, target.position, Currentforce, Physics.gravity);
        if (aimVector.HasValue)
        {
            Debug.Log(aimVector.Value.normalized);
            CanonBarrel.GetComponent<Rotation>().targetAngle = aimVector.Value.normalized;
            Debug.Log("Yes");
            CanonBarrel.GetComponent<Rotation>().AngleUpdate();
        }
    }

    public void spawnPrefab()
    {
        if (lastSpawnedObject != null)
        {
            Destroy(lastSpawnedObject);
        }
        GameObject spawnedPrefab = Instantiate(bulletPrefab, SpawnPos);
        lastSpawnedObject = spawnedPrefab;
        spawnedPrefab.transform.localPosition = Vector3.zero;
        spawnedPrefab.transform.localRotation = Quaternion.identity;

        // Freeze the rigidbody position and rotation of the spawned prefab
        Rigidbody rigidbody = spawnedPrefab.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        // Pass the target transform to the public variable of the spawned prefab
        var objectScript = spawnedPrefab.GetComponent<BulletSolver>();
        objectScript.targetPos = target;
        objectScript.launchForce = CurrentForce;

        cameraMan.GetComponent<SmoothCameraFollow>().target = spawnedPrefab.transform.Find("camTarget");
        cameraMan.GetComponent<SmoothCameraFollow>().lookTarget = spawnedPrefab.transform.Find("camLookTarget");
        //cameraMan.GetComponent<Camera>().enabled = false;
    }


}
