using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        // var newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);
        var newChunk = Instantiate(GetRandomChunk());
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].GetComponent<ChunkScript>().End.position - newChunk.GetComponent<ChunkScript>().Begin.localPosition;
        spawnedChunks.Add(newChunk);

        if (spawnedChunks.Count >= 3) 
        {
            Destroy(spawnedChunks[0]);
            spawnedChunks.RemoveAt(0);
        }
    }

    private GameObject GetRandomChunk()
    {
        // шансы всех кусков
        List<float> chanes = new List<float>();
        for (int i = 0; i < ChunkPrefabs.Length; i++)
        {
            chanes.Add(ChunkPrefabs[i].GetComponent<ChunkScript>().ChanceFromDistance.Evaluate(Player.transform.position.x));
        }

        // считаем сумму и получаем значение от 0 до этой суммы
        float value = Random.Range(0, chanes.Sum());

        // определяем в какой кусок попали
        float sum = 0;
        for (int i = 0; i < chanes.Count; i++)
        {
            sum += chanes[i];
            if (value < sum)
            {
                return ChunkPrefabs[i];
            }
        }

        // формально: недостижимый выход, т.к. дальше цикла проходить мы не должны
        return ChunkPrefabs[ChunkPrefabs.Length - 1];
    }
}
