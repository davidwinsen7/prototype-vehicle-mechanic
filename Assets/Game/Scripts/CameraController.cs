using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;

    public Vector3 offset;

    private void Update()
    {
        transform.position = followTarget.position + offset;
    }
}
