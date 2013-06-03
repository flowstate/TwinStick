using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{

    public List<GameObject> enemyPool;
    public List<SpawnSurface> surfaces;
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

    bool doneSpawining = false;
    bool allDead = false;

    public bool AllDone
    {
        get { return doneSpawining && allDead; }
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
    }

    private void InitSpawners()
    {
        foreach (SpawnSurface surf in surfaces)
        {
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
                SpawnEnemy();
        }

    }

    public bool CanSpawn()
    {
        if (!doneSpawining)
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

                    doneSpawining = true;
                }

            }
            return spawnResult;
        }
        else
        {
            if (liveEnemies == 0)
            {
                allDead = true;
                if (manager != null)
                {
                    manager.SpawnerFinished();
                }
            }
        }



        return false;
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
