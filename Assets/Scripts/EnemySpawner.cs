using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyType;
	public float spawnRadius = 10.0f;
	public float spawnDelay = 2;
	GameObject player;
	public  bool spawnFromEdge = true;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		//Invoke ("SpawnEnemy", spawnDelay);
	
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Circle")){
			Debug.Log("Spawning Enemy!");
			SpawnEnemy();
		}
	}
	
	void SpawnEnemy(){
		Vector3 spawnPosition;
		if(spawnFromEdge){
			spawnPosition = transform.position + ((Vector3)Random.insideUnitCircle).normalized*spawnRadius;
			spawnPosition.y = 0f;
		}else{
			spawnPosition = transform.position + ((Vector3)Random.insideUnitCircle)*spawnRadius;
		}
		
		GameObject enemyInstance = Instantiate(enemyType, spawnPosition, enemyType.transform.rotation) as GameObject;
		enemyInstance.transform.parent = transform;
		FollowPlayer script = enemyInstance.GetComponent<FollowPlayer>();
		script.SetTarget(player);
		//Invoke("SpawnEnemy", spawnDelay);
	}
	
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, spawnRadius);
	}
}
