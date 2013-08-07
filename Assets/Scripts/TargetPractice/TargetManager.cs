using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{

    public int PlayerOneScore = 0;
    public List<ShootingTarget> Targets;
    public int NumTargetsDown = 0;
    private Transform _transform;
    public int TotalTargets = 20;
    private int lowSpawnRange, highSpawnRange, activeTargets = 0;

    public bool UseTotalTargets = true,
                UseNumWaves = false,
                UseHighScore = false,
                UseTimedTargets = true;

    public float TargetTimer = 5;
    
	// Use this for initialization
	void Start ()
	{
	    _transform = transform;
	    InitTargets();
	    lowSpawnRange = Mathf.CeilToInt(TotalTargets/10);
	    highSpawnRange = Mathf.CeilToInt(TotalTargets/5);

        Debug.Log("Spawn Range: " + lowSpawnRange + " - " + highSpawnRange);

        StartCoroutine("SpawnTargetsTillTotal");
	}

    private void InitTargets()
    {
        foreach (Transform child in _transform)
        {
            ShootingTarget tempTarget = child.GetComponent<ShootingTarget>();
            if (null != tempTarget)
            {
                Targets.Add(tempTarget);
                tempTarget.Manager = this;
            }
        }
        Debug.Log("Total Targets Found: " + Targets.Count);
    }
	
    private IEnumerator SpawnTargetsTillTotal()
    {
        while (NumTargetsDown < TotalTargets)
        {
            SpawnTargets();
            yield return new WaitForSeconds(3);
        }
    }

    private void SpawnTargets()
    {
        Debug.Log("Spawning new targets");
        int numToSpawn = Random.Range(lowSpawnRange, highSpawnRange);
        

        int randomChild;
        
        if (numToSpawn > Targets.Count - activeTargets)
        {
            Debug.Log("Num to spawn too high");
            numToSpawn = Targets.Count - activeTargets;
        }
        Debug.Log("Num to Spawn: " +numToSpawn);

        while (numToSpawn > 0)
        {
            randomChild = Random.Range(0, Targets.Count - 1);

            if (!Targets[randomChild].isActive)
            {
                Targets[randomChild].EnableTimed(TargetTimer);
                numToSpawn--;
            }
            else
            {
                Debug.Log("Tried an active target");
            }
        }
    }

    public void TargetDown(GameObject hitter)
    {
        NumTargetsDown++;
        Mathf.Clamp(activeTargets--, 0, 500);
        Debug.Log("Number of Targets Down: " + NumTargetsDown);
        if (UseTotalTargets)
        {
            if (NumTargetsDown >= TotalTargets)
            {
                Debug.Log("DONE.");
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
