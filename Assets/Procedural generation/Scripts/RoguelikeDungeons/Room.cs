using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorU;
    public GameObject DoorL;
    public GameObject DoorR;
    public GameObject DoorD;

    public GameObject Center;

    public void RotateRandomly()
    {
        int count = Random.Range(0, 4);
        for (int i = 0; i < count; i++)
        {
            // transform.Rotate(0, 90, 0);
            transform.RotateAround(Center.transform.position, new Vector3(0, 1, 0), 90);

            GameObject doorTmp = DoorL;
            DoorL = DoorD;
            DoorD = DoorR;
            DoorR = DoorU;
            DoorU = doorTmp;
        }
    }
}
