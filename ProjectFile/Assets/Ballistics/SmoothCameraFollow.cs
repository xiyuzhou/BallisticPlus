using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{

    public Transform target;
    public float sSpeed = 10.0f;
    public Vector3 dist;
    public Transform lookTarget;
    public Transform canon;

    void Start()
    {
        target = canon.Find("Tip1");
        lookTarget = canon.Find("Tip2");
    }

    void FixedUpdate()
    {
        Vector3 dPos = target.position + dist;
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
        transform.position = sPos;
        transform.LookAt(lookTarget.position);
    }
    public void reset()
    {
        target = canon.Find("Tip1");
        lookTarget = canon.Find("Tip2");
    }
}