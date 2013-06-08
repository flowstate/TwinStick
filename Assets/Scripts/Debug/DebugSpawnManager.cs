using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DebugSpawnManager : MonoBehaviour {

    public List<DebugSpawner> spawners;
    public bool enemiesDone = false;
    int currentSpawner = -1;

    // Use this for initialization
    void Start()
    {
        StartSpawns();
    }

    public void StartSpawns()
    {
        Debug.Log("Spawn Started.");
        CycleSpawners();

    }

    public void SpawnerFinished()
    {
        CycleSpawners();
    }

    private void CycleSpawners()
    {
       
    }

    private void InitSpawners()
    {
        if (spawners != null)
        {
            foreach (DebugSpawner spawn in spawners)
            {
                //StartCoroutine(spawn.TimedSpawn());
            }

            Debug.Log("I spawned em!");

        }
        else
        {
            Debug.Log("Do it different, cappin!");

        }
    }

}
