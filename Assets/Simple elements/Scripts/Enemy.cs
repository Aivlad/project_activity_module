using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int weight = 20;
    public float speed = 0.07f;
    public Vector3[] positions;
    private int currentTarget;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, positions[currentTarget], speed);

        if (transform.position == positions[currentTarget]) 
        {
            if (currentTarget < positions.Length - 1) 
            {
                currentTarget++;
            }
            else 
            {
                currentTarget = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Игрок убит");
            SceneManager.LoadScene("GameScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().AddCoin(weight);
            // Debug.Log("Противник убит");
            Destroy(gameObject);
        }
    }
}
