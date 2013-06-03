using UnityEngine;
using System.Collections;

public class SelectiveExplodeOnCollision : MonoBehaviour {

	public GameObject explosionDebris;
	public GameObject explosionObject;
	public int amount = 10;
	public float spawnRadius = 0.5f;
	public float explosionRadius = 1.0f;
	public float explosionForce = 500f;
	Transform _transform;
	
	public LayerMask layerMask;
	
	// Use this for initialization
	void Start () {
		_transform = transform;
	
	}
	
	
	void OnCollisionEnter(Collision collision){
		if(IsInLayerMask(collision.gameObject) ){
		
			for(int i = 0; i < amount; ++i){
				Vector3 spawnPosition = _transform.position + Random.onUnitSphere*spawnRadius;
				GameObject debrisInstance = Instantiate(explosionDebris, spawnPosition, explosionDebris.transform.rotation) as GameObject;
				debrisInstance.rigidbody.AddExplosionForce(explosionForce, _transform.position,explosionRadius);
			}
		
			Instantiate(explosionObject, _transform.position, explosionObject.transform.rotation);
		
			Destroy(gameObject);
		}
	}
	
	private bool IsInLayerMask(GameObject obj){
		int objLayerMask = (1 << obj.layer);
		if((layerMask.value & objLayerMask) > 0)
			return true;
		return false;
	}
}
