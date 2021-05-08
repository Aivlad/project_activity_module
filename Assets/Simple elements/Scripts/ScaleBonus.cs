using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().AddScaleBonus();
            
            Destroy(gameObject);
        }
    }
}
