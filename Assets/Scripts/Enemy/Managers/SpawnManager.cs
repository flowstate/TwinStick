using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnManager : MonoBehaviour {

    public List<Spawner> spawners;
    public bool enemiesDone = false;
    int currentSpawner = -1;

	// Use this for initialization
	void Start () 
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
        if (currentSpawner < spawners.Count - 1 )
        {
            spawners[++currentSpawner].BeginSpawning();
            
            
            
        }
        else
        {
            Debug.Log("Enemies done. End of line");

            enemiesDone = true;
        }
    }

    private void InitSpawners()
    {
        if (spawners != null)
        {
            foreach (Spawner spawn in spawners)
            {
                StartCoroutine(spawn.TimedSpawn());
            }

            Debug.Log("I spawned em!");

        }
        else
        {
            Debug.Log("Do it different, cappin!");

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
