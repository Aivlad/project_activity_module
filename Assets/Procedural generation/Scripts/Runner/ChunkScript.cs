using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkScript : MonoBehaviour
{
    public Transform Begin;
    public Transform End;

    public Material[] BlockMaterials;    // material, texture, mesh ...

    public AnimationCurve ChanceFromDistance;   // шанс в зависимости от пройденной дистанции

    private void Start()
    {
        foreach (var filter in GetComponentsInChildren<MeshRenderer>()) 
        {
            filter.material = BlockMaterials[Random.Range(0, BlockMaterials.Length)];   // случайны материал
            filter.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);    // случайный угол поворота
        }
    }

}
