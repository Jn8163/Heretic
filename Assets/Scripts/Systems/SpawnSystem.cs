using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private List<Spawn> spawnPoints;
    
    private void Start()
    {
        foreach (Spawn spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnObject();
        }
    }
}