using UnityEngine;
using System.Collections;

public class ProjectileWaveSpawner : MonoBehaviour
{

    public int SpawnNum = 5;
    public float SpawnDistance = 5f;
    public Vector3 ProjectileDirection;
    public GameObject GoodSpawn, DeathSpawn;
    public int MinGoodSpawn;
    public float SpawnDelay;
    public bool CanSpawn = true;
    private Vector3 StartingPoint;
    private Transform _transform;
    public float SpawnStart;
	// Use this for initialization
	void Start ()
	{
	    _transform = transform;
	    CalculateStart();
        StartCoroutine(TimedSpawn());

	}

    private void CalculateStart()
    {
         SpawnStart = _transform.position.x - (Mathf.FloorToInt(SpawnNum / 2f) * SpawnDistance);
        Debug.Log("Starting point X: " + SpawnStart.ToString("0.00"));
        if (SpawnNum % 2 == 0)
        {
            SpawnStart += SpawnDistance / 2;
        }
    }


	
    public IEnumerator TimedSpawn()
    {
        while (CanSpawn)
        {
            yield return new WaitForSeconds(SpawnDelay);
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        Vector3 spawnPoint;

        for (int i = 0; i < SpawnNum; i++)
        {
            spawnPoint = new Vector3(SpawnStart + (SpawnDistance * i), _transform.position.y, _transform.position.z);
            GameObject child = Instantiate(GoodSpawn, spawnPoint, Quaternion.identity) as GameObject;
            child.rigidbody.AddForce(_transform.forward * 10f, ForceMode.VelocityChange );
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
