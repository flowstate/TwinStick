using UnityEngine;
using System.Collections;

public class RotateTowardPlayer : MonoBehaviour {
	
	public float rotateSpeed = 5f;
	public GameObject target;
	Transform targetTransform, _transform;
	Vector3 aimVector;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		targetTransform = target.transform;
		_transform = transform;
	}
	
	void Update(){
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
}
