using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    private Vector3 attitude;
    private void Start()
    {
        attitude = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + attitude;
    }
}
