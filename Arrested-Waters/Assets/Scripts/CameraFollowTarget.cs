using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;

    public Vector3 offset;

    public void Start()
    {
        target = GameManager.instance.player.transform;
    }
    void FixedUpdate()
    {
        Vector3 finalPos = target.position + offset;    // This is stuck camera.
        Vector3 smoothPos = Vector3.Lerp(transform.position, finalPos, smoothSpeed * Time.deltaTime);   // This is smooth camera.
        transform.position = smoothPos;
    }
}
