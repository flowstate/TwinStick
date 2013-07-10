using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class DebugSpawner : MonoBehaviour {

    public List<GameObject> enemyPool;
    public List<SpawnSurface> surfaces;
    public SpawnManager manager;

    [HideInInspector]
    public int liveEnemies;
    [HideInInspector]
    public int totalSpawned = 0;

    public int spawnIndex = 0;

    public List<GameObject> Spawnables; 

    public LayerMask collisionMask;
    public GameObject childTarget;

    public float startDelay = 0.1f;
    public int maxAlive = -1;
    public int spawnCap = -1;
    public bool SpawnAutomatically = false;
    public int maxSimultaneousSpawn = 5;
    bool doneSpawining = false;
    bool allDead = false;
    private bool ScaleByManager = false;

    // Use this for initialization
    void Start()
    {
        ErrorChecks();
        InitSpawners();

        if (SpawnAutomatically)
        {
            StartCoroutine(TimedSpawn());
        }
        
    }

    void SpawnEnemy(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnMax();
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

    public IEnumerator TimedSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f,2.0f));
            SpawnEnemy();
        }
    }

    private void ErrorChecks()
    {
        if (Spawnables == null || Spawnables.Count == 0)
        {
            throw new NullReferenceException("You have not set spawnables on " + gameObject.name);
        }
    }

    private void InitSpawners()
    {
        foreach (SpawnSurface surf in surfaces)
        {
            surf.objectToSpawn = Spawnables[spawnIndex];
            surf.target = childTarget;
            surf.owner = gameObject;
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemy();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            ChangeSpawnable(false);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            ChangeSpawnable(true);
        }

        // update number of children
        UpdateLiveChildren();

    }

    void ChangeSpawnable(bool increment)
    {
        if (increment && spawnIndex < Spawnables.Count - 1)
        {
            spawnIndex++;
            ChangeChildSpawns();
        }
        if (!increment && spawnIndex > 0)
        {
            spawnIndex--;
            ChangeChildSpawns();
        }
    }

    private void ChangeChildSpawns()
    {
        foreach (SpawnSurface surface in surfaces)
        {
            surface.objectToSpawn = Spawnables[spawnIndex];
        }
    }

    void UpdateLiveChildren()
    {
        liveEnemies = transform.childCount;
    }

    void SpawnEnemy()
    {
        Debug.Log("Spawning Enemy");

        // TODO: pick random
        int index = Random.Range(0, surfaces.Count - 1);
        Debug.Log("Chosen index: " + index);

        while (surfaces[index].IsFlashing /*|| surfaces[index].SeatsTaken()*/)
        {
            index = Random.Range(0, surfaces.Count - 1);
        }

        Debug.Log("Telling surface to spawn");

        surfaces[index].FlashUp();
        totalSpawned++;

    }

    GameObject PickRandomEnemy()
    {
        GameObject chosen = null;

        return chosen;
    }
}
