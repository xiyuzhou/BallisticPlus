using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletSolver : MonoBehaviour
{
    public Transform targetPos;
    public float myTimeScale = 1.0f;
    public float launchForce;
    Rigidbody rb;
    Vector3 startPos;
    public GameObject cameraMan;
    private Vector3 oldPosition;

    void Start()
    {
        startPos = transform.position;
        Time.timeScale = myTimeScale; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();
        oldPosition = transform.position;
        cameraMan = GameObject.Find("FollowBullet");
        
    }

    void EnableSphereCollider()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = true;
    }

    public void lauching()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("go");
        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = fs.Calculate(transform.position, targetPos.position, launchForce, Physics.gravity);
        Debug.Log(aimVector);
        if (aimVector.HasValue)
        {
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.enabled = false;

            rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
            Invoke("EnableSphereCollider", 0.5f);
            sphereCollider.enabled = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag != "Canon")
        {
            Debug.Log("1");
            Destroy(this.gameObject, 1f);
        }
        
    }
    

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(oldPosition, transform.position - oldPosition, out hit, (transform.position - oldPosition).magnitude))
        {
            if (hit.collider.gameObject.tag != "Canon" && hit.collider.gameObject.tag != "Bullet")
            {
                Debug.Log(hit.collider.gameObject.tag);
                Destroy(this.gameObject);
            }
        }
        oldPosition = transform.position;
    }

    private void destroyObject()
    {
    }


    void OnDestroy()
    {
        cameraMan.GetComponent<SmoothCameraFollow>().reset();
    }
}
