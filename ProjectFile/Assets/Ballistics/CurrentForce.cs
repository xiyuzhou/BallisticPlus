using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentForce : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    void Start()
    {
        textComponent.text = "Current Force: 0";
    }

    // Update is called once per frame
    public void UpdateText(float value)
    {
        textComponent.text = "Current Force: " + value;
    }
}
