using UnityEngine;
using System.Collections;

public class TriggerRoom : MonoBehaviour {

    public GameObject manager;
    RoomManager rManager;
    SpawnManager sManager;

    bool triggerPulled, roomTriggered, spawnTriggered;
	// Use this for initialization
	void Start () {
        roomTriggered = spawnTriggered = triggerPulled = false;
        InitManagers();
	}

    private void InitManagers()
    {
        rManager = manager.GetComponent<RoomManager>();
        sManager = manager.GetComponent<SpawnManager>();
    }
	
	// Update is called once per frame
    void Update()
    {
        if (roomTriggered)
        {
            if (rManager.FinishedRaising)
            {
                roomTriggered = false;
                TriggerSpawns();
            }
        }
	}

    void OnTriggerExit(Collider other) {
        if (!triggerPulled)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TriggerRoomManager();
                triggerPulled = true;
            }
        }
        
    }

    void TriggerRoomManager()
    {
        rManager.RaiseWalls();
        roomTriggered = true;
    }

    void TriggerSpawns()
    {
        Debug.Log("Spawns Triggered");
        spawnTriggered = true;
        Destroy(gameObject);

    }
}
