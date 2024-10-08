using Sample;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> GargoyleSpawns = new List<GameObject>();
    [SerializeField] GameObject GargoylePrefab;
    int GarSpawnNum;
    void Start()
    {
        GarSpawnNum = -1;
        foreach(GameObject g in GargoyleSpawns)
        {
            Instantiate(GargoylePrefab, GargoyleSpawns[GarSpawnNum + 1].transform.position, GargoyleSpawns[GarSpawnNum + 1].transform.rotation);
            GarSpawnNum++;
        }
    }

}
