using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacerScript : MonoBehaviour
{
    public Transform Player;    // игрок (для определения его позиции)
    public GameObject[] ChunkPrefabs;   // доступные куски 
    private List<GameObject> spawnedChunks = new List<GameObject>();  // список уже заспавленных кусков

    public GameObject FirstChunk;

    private void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    private void Update()
    {
        if (Player.position.x > spawnedChunks[spawnedChunks.Count - 1].GetComponent<ChunkScript>().End.position.x - 5)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        var newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].GetComponent<ChunkScript>().End.position - newChunk.GetComponent<ChunkScript>().Begin.localPosition;
        spawnedChunks.Add(newChunk);

        if (spawnedChunks.Count >= 3) 
        {
            Destroy(spawnedChunks[0]);
            spawnedChunks.RemoveAt(0);
        }
    }
}
