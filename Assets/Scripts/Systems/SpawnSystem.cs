using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour, IManageData
{
    [SerializeField] private List<Spawn> spawnPoints;
    
    public void LoadData(GameData data)
    {
        foreach (Spawn spawn in spawnPoints)
        {
            if(data.spawnPointStatues.ContainsKey(spawn.id))
            {
                spawn.targetActive = data.spawnPointStatues[spawn.id];
                spawn.SpawnObject();
            }
        }
    }



    public void SaveData(ref GameData data)
    {
        foreach (Spawn spawn in spawnPoints)
        {
            if (data.spawnPointStatues.ContainsKey(spawn.id))
            {
                data.spawnPointStatues[spawn.id] = spawn.targetActive;
            }
            else
            {
                data.spawnPointStatues.Add(spawn.id, spawn.targetActive);
            }
        }
    }
    


    private void Start()
    {
        foreach (Spawn spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnObject();
        }
    }
}