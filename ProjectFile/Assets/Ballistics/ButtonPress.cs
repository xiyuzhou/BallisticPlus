using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour, InInteractable
{
    public Transform buttonT;
    public Transform Tip;
    IEnumerator waiter()
    {
        var a = buttonT.position;
        buttonT.position = Tip.position;
        LanuchTrigger();
        yield return new WaitForSeconds(1f);
        buttonT.position = a;
    }
    public void iInteract()
    {
        StartCoroutine(waiter());
    }

    public void LanuchTrigger()
    {
        GameObject bulletObject = GameObject.FindWithTag("Bullet");
        bulletObject.GetComponent<BulletSolver>().lauching();
    }
}