using UnityEngine;
using System.Collections;

public class GibOnCollide : MonoBehaviour {
	
	public GameObject gib;
	public int amount = 10;
	public float spawnRadius = 0.5f;
	public float explosionRadius = 1;
	public float explosionForce = 500;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision){
		
		for(int i = 0; i < amount; ++i){
			Vector3 spawnPosition = transform.position + Random.onUnitSphere * spawnRadius;
			GameObject gibInstance = Instantiate(gib, spawnPosition, gib.transform.rotation) as GameObject;
			gibInstance.rigidbody.AddExplosionForce(explosionForce,transform.position, explosionRadius);
		}
		Destroy(gameObject);
		
	}
}
