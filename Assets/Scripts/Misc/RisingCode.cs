using UnityEngine;
using System.Collections;

public class RisingCode : MonoBehaviour {

    public float timeToRise = 0.5f;
    public float spawnDelay = 0.5f;
    GameObject childSpawn, caller, childTarget;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Rise(Vector3 offset, GameObject mCaller, GameObject toSpawn, GameObject target)
    {
        transform.parent = mCaller.transform;
        childSpawn = toSpawn;
        caller = mCaller;
        // animate the rise
        childTarget = target;

        Hashtable spawnTable = new Hashtable();
        spawnTable.Add("time", timeToRise);
        spawnTable.Add("position", offset);
        spawnTable.Add("oncomplete", "DoneRising");


        iTween.MoveTo(gameObject, spawnTable);
        
        // set active to false for child
        // start coroutine to wait then activate child

    }

    void DoneRising() 
    {
        StartCoroutine(SpawnStart());
    }

    IEnumerator SpawnStart()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnChild();
    }

    void SpawnChild() 
    {
        GameObject spawned = Instantiate(childSpawn, transform.position, transform.rotation) as GameObject;
        spawned.transform.parent = caller.transform;

        Enemy spawnedEnemy = spawned.GetComponent<Enemy>();

        if (spawnedEnemy != null)
        {
            spawnedEnemy.target = childTarget;
        }

        Destroy(gameObject);
    }
}
