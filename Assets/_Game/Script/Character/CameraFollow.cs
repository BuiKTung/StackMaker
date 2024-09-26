using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position + offset, 1f);
        transform.position = smoothedPosition;
    }
}
