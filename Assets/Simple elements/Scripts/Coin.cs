using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int weight = 10;

    private GameObject player;
    private AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // если пересечение с игроком
        if (collider.tag == "Player")
        {
            // добавляем очки
            collider.GetComponent<Player>().AddCoin(weight);
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject);
        }
    }
}
