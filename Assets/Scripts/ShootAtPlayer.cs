using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour {
	
	public GameObject target;
	Transform targetTransform;
	Transform _transform;
	Vector3 fireDirection;
	public float fireDelay = 2f;
	public GameObject bullet;
	public float bulletSpeed = 10;
	// Rigidbody _rigidbody;
	
	// Use this for initialization
	void Start () {
		_transform = transform;	
		SetTarget(target);
		StartCoroutine(FiringTimer());
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	IEnumerator FiringTimer(){
		while(true){
			if(target != null){
				Fire();		
				yield return new WaitForSeconds(fireDelay);
			} else{
				Debug.Log("Target is null");
				yield return null;
			}
			
		}
	}
	
	void Fire(){
		Debug.Log("Fire called");
		fireDirection = (targetTransform.position - _transform.position).normalized;
		GameObject bulletInstance = Instantiate(bullet, _transform.position, Quaternion.LookRotation(fireDirection)) as GameObject;
		bulletInstance.rigidbody.AddForce(fireDirection * bulletSpeed,ForceMode.VelocityChange);
	}
	
	public void SetTarget(GameObject newTarget){
		if(newTarget != null){
			target = newTarget;
			targetTransform = target.transform;
		}
	}
}
