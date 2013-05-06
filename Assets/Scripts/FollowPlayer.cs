using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	public float speed = 1;
	public float rotateSpeed = 5f;
	public float maxAcceleration = 1;
	public float maxVelocity = 3;
	public GameObject target;
	TwinStickShipZed targetShip;
	Transform targetTransform = null;
	int amount = 11;
	Transform _transform;
	Rigidbody _rigidbody;
	Vector3 aimVector;
	
	// Use this for initialization
	void Start () {
		_transform = transform;
		_rigidbody = rigidbody;
		SetTarget(target);
		targetShip = target.GetComponent<TwinStickShipZed>();
	}
	
	// Update is called once per frame
	void Update () {
		
		RotateTowardsTarget();
	}
	
	
	void FixedUpdate(){
		if(target != null){
			
			if(targetShip.hasCaptured){
				FleeTight();
			}else{
				SeekTight();
			}
			
			
		}
	}
	
	public void RotateTowardsTarget(){
		if(target != null){
			// calculate aim vector as the way we would be pointing to be looking at the player
		aimVector = targetTransform.position - _transform.position;
		//_transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(_transform.forward, targetTransform.position - _transform.position, rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime),_transform.up);
		
		
		_transform.rotation = Quaternion.RotateTowards(
			// Rotate from current rotation (lookRotation) 
			Quaternion.LookRotation(_transform.forward, _transform.up),
			// Towards the rotation we would need to be looking at the player
			Quaternion.LookRotation(aimVector, _transform.up),
			// At a rate of rotateSpeed (radians) * radian to degree ratio * elapsed time
			rotateSpeed * Mathf.Rad2Deg * Time.deltaTime
			);
		}
	}
	
	public void SeekTight(){
		Vector3 desiredDirection = (targetTransform.position - _transform.position).normalized * maxVelocity *maxVelocity;
								
		_rigidbody.AddForce(_rigidbody.velocity + desiredDirection);
		_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
	}
	
	public void FleeTight(){
		Vector3 desiredDirection = (_transform.position - targetTransform.position).normalized * maxVelocity *maxVelocity;
								
		rigidbody.AddForce(rigidbody.velocity + desiredDirection);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
	}
	
	public void SetTarget(GameObject newTarget){
		if(newTarget != null){
			target = newTarget;
			targetTransform = target.transform;
		}
		
	}
	
	void OnCollisionEnter(Collision collision){
		
		if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
			
		}
		else{
			//for(int i = 0; i < amount; ++i){
			//	Vector3 spawnPosition = transform.position + Random.onUnitSphere * spawnRadius;
			//	GameObject gibInstance = Instantiate(gib, spawnPosition, gib.transform.rotation) as GameObject;
			//	gibInstance.rigidbody.AddExplosionForce(explosionForce,transform.position, explosionRadius);
			//}
			Destroy(gameObject);
		}
		
		
	}
}
