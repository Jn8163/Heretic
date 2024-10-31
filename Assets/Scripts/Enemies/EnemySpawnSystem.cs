using Sample;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> GargoyleSpawns = new List<GameObject>();
    [SerializeField] GameObject GargoylePrefab;
    int SpawnNum;
    [SerializeField]
    public List<GameObject> GolemSpawns = new List<GameObject>();
    //golem prefab
    [SerializeField]
    public List<GameObject> SkeletonSpawns = new List<GameObject>();
    //skeleton warrior prefab

    void Start()
    {
        SpawnNum = -1;
        foreach(GameObject g in GargoyleSpawns)
        {
            Instantiate(GargoylePrefab, GargoyleSpawns[SpawnNum + 1].transform.position, GargoyleSpawns[SpawnNum + 1].transform.rotation);
            SpawnNum++;
        }
        SpawnNum = -1;
        foreach( GameObject g in GolemSpawns)
        {
            Instantiate(GargoylePrefab, GolemSpawns[SpawnNum + 1].transform.position, GolemSpawns[SpawnNum + 1].transform.rotation);
            SpawnNum++;
        }
        SpawnNum = -1;
        foreach (GameObject g in SkeletonSpawns)
        {
            Instantiate(GargoylePrefab, SkeletonSpawns[SpawnNum + 1].transform.position, SkeletonSpawns[SpawnNum + 1].transform.rotation);
            SpawnNum++;
        }
        SpawnNum = -1;
    }

}
