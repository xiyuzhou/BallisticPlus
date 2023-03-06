using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r,out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log(hitInfo.collider.gameObject);
                if (hitInfo.collider.gameObject.TryGetComponent(out InInteractable interactObj))
                {
                    Debug.Log("hit");
                    interactObj.iInteract();
                }
            }
        }
    }
}

interface InInteractable
{
    public void iInteract();
}
