using UnityEngine;
using System.Collections;

public class OrbitAroundTarget: MonoBehaviour {
	
	public GameObject target;
	Transform _transform, targetTransform;
	bool rotateClockwise = true;
	public float rotateSpeed = 10;
	public float orbitDistance = 5;
	public float chaseSpeed = 10;
	float sqrOrbitDistance;
	bool withinDistance = false;
	// Use this for initialization
	void Start () {
		_transform = transform;
		SetTarget(target);
		sqrOrbitDistance = orbitDistance * orbitDistance;
	}
	
	// Update is called once per frame
	void Update () {
		// if we're close enough to the target
		if(CheckWithinDistance()){
			Orbit ();
		}
		// we're still too far away
		else{
			ChaseTarget();
		}
		
		// stupid version
		//_transform.RotateAround(Vector3.zero, _transform.forward, rotateSpeed*Time.deltaTime);
		
	
	}
	
	public void SetTarget(GameObject newTarget){
		if(newTarget != null){
			target = newTarget;
			targetTransform = target.transform;
		}
		
	}
	
	void ChaseTarget(){
		Vector3 moveDirection = (targetTransform.position - _transform.position).normalized;
			_transform.position += moveDirection * Time.deltaTime * chaseSpeed;
	}
	
	void Orbit(){
		_transform.RotateAround(targetTransform.position,_transform.forward,rotateSpeed*Time.deltaTime);
	}
	
	bool CheckWithinDistance(){
		if(target != null){
			// if the distance between us and the target is greater than specified
			if((targetTransform.position - _transform.position).sqrMagnitude > sqrOrbitDistance){
				return false;
			}
				
			return true;
		}
		// target is null
		else{
			return true;
		}
	}
}
