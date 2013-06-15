using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnManager : MonoBehaviour {

    public List<Spawner> spawners;
    public bool enemiesDone = false;
    int currentSpawner = -1;
    public bool isLastSpawnerBoss = false;
    public GameObject LoadingScreen;
    public float bossDelay = 2.0f;
    public GameObject player;
	// Use this for initialization
	void Start () 
    {
        //StartSpawns();    
	}

    void Update()
    {
        //if (CheckSpawns())
        //{
        //    SpawnerFinished();
        //}
    }

    public void StartSpawns()
    {
        Debug.Log("Spawn Started.");
        CycleSpawners();
        
    }

    public bool CheckSpawns()
    {
        return spawners[currentSpawner].AllDone;
    }

    public void SpawnerFinished()
    {
        CycleSpawners();
    }

    private void CycleSpawners()
    {
        
        if (currentSpawner < spawners.Count - 1 )
        {
            currentSpawner++;
            
            if (currentSpawner == spawners.Count - 1 && isLastSpawnerBoss)
            {
                
                StartCoroutine(InitBossSpawn());
            }
            else
            {
                spawners[currentSpawner].BeginSpawning();    
            }

            
        }
        else
        {
            Debug.Log("Enemies done. End of line");

            enemiesDone = true;
        }
    }

    IEnumerator InitBossSpawn()
    {
        // activate the loading screen
        LoadingScreen.SetActive(true);
        
        // start player tween
        player.SendMessage("Tween", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(2.0f);

        // deactivate the loading screen
        LoadingScreen.SetActive(false);

        // begin boss spawn
        spawners[currentSpawner].BeginSpawning();
    }

    private void InitSpawners()
    {
        if (spawners != null)
        {
            foreach (Spawner spawn in spawners)
            {
                StartCoroutine(spawn.TimedSpawn());
            }


        }
        else
        {
            throw  new NullReferenceException("Spawners aren't set in the SpawnManager");
        }
    }
	
}
