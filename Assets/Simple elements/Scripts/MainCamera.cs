using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform trackingObject;
    public float leftBorderLevel = -9;
    public float rightBorderLevel = 24;

    private void Update()
    {
        var valX = trackingObject.position.x;
        if (valX < leftBorderLevel)
        {
            transform.position = new Vector3(leftBorderLevel, 0, -10);
        }
        else if (valX > rightBorderLevel) 
        {
            transform.position = new Vector3(rightBorderLevel, 0, -10);
        }
        else 
        {
            transform.position = new Vector3(trackingObject.position.x, 0, -10);
        }
    }
}
