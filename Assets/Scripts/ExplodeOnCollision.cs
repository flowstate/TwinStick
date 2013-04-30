using UnityEngine;
using System.Collections;

public class ExplodeOnCollision : MonoBehaviour {
	
	public GameObject explosionDebris;
	public GameObject explosionObject;
	public int amount = 10;
	public float spawnRadius = 0.5f;
	public float explosionRadius = 1.0f;
	public float explosionForce = 500f;
	Transform _transform;
	
	// Use this for initialization
	void Start () {
		_transform = transform;
	
	}
	
	
	void OnCollisionEnter(Collision collision){
		
		for(int i = 0; i < amount; ++i){
			Vector3 spawnPosition = _transform.position + Random.onUnitSphere*spawnRadius;
			GameObject debrisInstance = Instantiate(explosionDebris, spawnPosition, explosionDebris.transform.rotation) as GameObject;
			debrisInstance.rigidbody.AddExplosionForce(explosionForce, _transform.position,explosionRadius);
		}
	
		Instantiate(explosionObject, _transform.position, explosionObject.transform.rotation);
	
		Destroy(gameObject);
		
	}
}
