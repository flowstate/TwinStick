using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public List<GameObject> enemyPool;
    public List<SpawnSurface> surfaces;
    public SpawnSurface bossSurface;
    public GameObject bossToSpawn;
    public SpawnManager manager;

    [HideInInspector]
    public int liveEnemies;
    [HideInInspector]
    public int totalSpawned = 0;

    public LayerMask collisionMask;
    public GameObject childTarget;

    public float startDelay = 0.1f;
    public int maxAlive = -1;
    public int spawnCap = -1;
    public int maxSimultaneousSpawn = 3;
    bool doneSpawning = false;
    bool allDead = false;

    public bool AllDone
    {
        get { return doneSpawning && allDead;}
        set { }
    }

    public float spawnTimer = 2.0f;

    private bool seatsTaken = false;

    // Use this for initialization
    void Start()
    {

        InitSpawners();

    }

    public void BeginSpawning()
    {
        StartCoroutine(TimedSpawn());

        if (bossSurface != null)
        {
            bossSurface.FlashUp();
        }
    }

    private void InitSpawners()
    {
        foreach (SpawnSurface surf in surfaces)
        {
            surf.target = childTarget;
            surf.owner = gameObject;
        }

        if (bossSurface != null)
        {
            if (bossToSpawn == null)
            {
                throw new NullReferenceException("You haven't set a boss to spawn, dingus.");
            }
            else
            {
                bossSurface.objectToSpawn = bossToSpawn;
            }
            bossSurface.owner = gameObject;
            bossSurface.target = childTarget;
        }
    }

    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (Transform kid in transform)
            {
                Destroy(kid.gameObject);
            }
        }

        // update number of children
        UpdateLiveChildren();

    }

    public IEnumerator TimedSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (!AllDone)
        {
            yield return new WaitForSeconds(spawnTimer);

            if (CanSpawn())
                SpawnMax();
        }
        
        manager.SpawnerFinished();
        yield return null;

    }

    public bool CanSpawn()
    {
        // if we're not done spawning yet
        if (!doneSpawning)
        {
            bool spawnResult = true;

            // if we have a maxAlive restrictor
            if (maxAlive != -1)
            {
                // check that we don't already have too many alive
                if (liveEnemies >= maxAlive)
                    spawnResult = false;
            }

            // if we have a cap on total spawns
            if (spawnCap != -1)
            {
                if (totalSpawned >= spawnCap)
                {
        
                    spawnResult = false;

                    doneSpawning = true;
                }

            }
            return spawnResult;
        }
        // we ARE done spawning, let's make sure they're all alive
        else
        {
            if (liveEnemies == 0)
            {
                allDead = true;
        
                if (manager != null)
                {
                    //manager.SpawnerFinished();
                }
            }
        }



        return false;
    }

    void UpdateLiveChildren()
    {
        liveEnemies = transform.childCount;
    }

    void SpawnEnemy(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnMax()
    {
        // if we have a maximum cap of children to have alive at once
        if (maxAlive != -1)
        {
            int upperLimit = (maxSimultaneousSpawn < maxAlive - liveEnemies)
                                 ? maxSimultaneousSpawn
                                 : maxAlive - liveEnemies;

            SpawnEnemy(Random.Range(1, upperLimit));
        }
        else
        {
            SpawnEnemy(Random.Range(1, maxSimultaneousSpawn));
        }
    }

    void SpawnEnemy()
    {

        int index = Random.Range(0, surfaces.Count - 1);


        while (surfaces[index].IsFlashing /*|| surfaces[index].SeatsTaken()*/)
        {
            index = Random.Range(0, surfaces.Count - 1);
        }


        surfaces[index].objectToSpawn = SelectEnemyToSpawn();
        surfaces[index].FlashUp();
        totalSpawned++;

    }

    private GameObject SelectEnemyToSpawn()
    {
        int index = Random.Range(0, enemyPool.Count - 1);
        return enemyPool[index];
    }

    GameObject PickRandomEnemy()
    {
        GameObject chosen = null;

        return chosen;
    }
}
