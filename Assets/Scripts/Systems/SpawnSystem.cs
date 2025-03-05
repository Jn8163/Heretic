using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour, IManageData
{
    [SerializeField] private List<Spawn> spawnPoints;
    
    public void LoadData(GameData data)
    {
        foreach(KeyValuePair<string, bool> spawnPoint in data.spawnPointStatues)
        {
            foreach (Spawn spawn in spawnPoints)
            {
                if(spawn.id.CompareTo(spawnPoint.Key) == 0)
                {
                    spawn.targetActive = spawnPoint.Value;
                    spawn.SpawnObject();
                }
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