using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

	public bool spawnRandomly = false;
	List<RisingWall> childWalls;
    bool raiseInProgress, lowerInProgress, spawnInProgress;
    public bool FinishedRaising{set;get;}
    public bool FinishedSpawning { set; get; }
    public bool FinishedLowering{set;get;}
    SpawnManager sManager;

	// Use this for initialization
	void Start () {
        sManager = gameObject.GetComponent<SpawnManager>();
		childWalls = new List<RisingWall>();
		InitWalls();

	}

	void InitWalls()
	{
        raiseInProgress = lowerInProgress = FinishedLowering = FinishedRaising = false;

		foreach (Transform child in transform)
		{
			RisingWall tempWall = child.GetComponent<RisingWall>();
			if (tempWall != null)
			{
				childWalls.Add(tempWall);

			}
		}
	}

	public void RaiseWalls()
	{
		foreach (RisingWall wall in childWalls)
		{
            wall.BeginRaise();

		}

        raiseInProgress = true;
        FinishedRaising = false;
	}

    public void LowerWalls(){
        foreach (RisingWall wall in childWalls)
        {
            wall.BeginLower();

        }
        lowerInProgress = true;
        FinishedLowering = false;
    }

	// Update is called once per frame
	void Update () {
        if (raiseInProgress)
        {
            if (CheckRaise())
            {
                FinishedRaising = true;
                raiseInProgress = false;
                StartSpawns();
            }
                
        }

        if (spawnInProgress)
        {
            if (sManager.enemiesDone)
            {
                LowerWalls();
            }
        }

        else if (lowerInProgress)
        {
            if (CheckLower())
            {
                FinishedLowering = true;
                lowerInProgress = false;
            }
                
        }
	}

    private void StartSpawns()
    {
        spawnInProgress = true;
        sManager.StartSpawns();
    }

    bool CheckRaise()
    {
        bool finished = true;
        foreach (RisingWall wall in childWalls)
        {
            if (!wall.WallUp)
                finished = false;
        }
        return finished;
    }

    bool CheckLower()
    {
        bool finished = true;
        foreach (RisingWall wall in childWalls)
        {
            if (wall.WallUp)
                finished = false;
        }

        return finished;
    }
}
