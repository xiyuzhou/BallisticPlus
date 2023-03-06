using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanonInteractable : MonoBehaviour
{
    public TextMeshProUGUI interactCanvas;
    public GameObject Panel;
    public bool inrange = false;
    public bool canvasOpen = false;
    public bool mapOpen = false;
    private void interactable()
    {
        Panel.SetActive(true);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactCanvas.enabled = true;
            Debug.Log("entered");
            inrange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactCanvas.enabled = false;
            Panel.SetActive(false);
            inrange = false;
        }
    }

    void Start()
    {
        Panel.SetActive(false);
        interactCanvas.enabled = false;
    }

    void LateUpdate()
    {
        if (inrange)
        {
            if (Input.GetKeyDown(KeyCode.E)&& mapOpen == false)
            {
                canvasOpen = !canvasOpen;
                if (canvasOpen)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0f;
                    interactCanvas.enabled = false;
                    Panel.SetActive(true);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1f;
                    interactCanvas.enabled = true;
                    Panel.SetActive(false);
                }
                    
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                mapOpen = !mapOpen;
                if (mapOpen)
                {
                    interactCanvas.enabled = false;
                    Panel.SetActive(false);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0f;
                    interactCanvas.enabled = false;
                    Panel.SetActive(true);
                }
            }
        }        
    }
    public void exitToScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        interactCanvas.enabled = true;
        Panel.SetActive(false);
    }
}
