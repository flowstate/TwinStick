using UnityEngine;
using System.Collections;

public class TwinStickShip : MonoBehaviour {
	
	public float moveSpeed = 5;
	Transform _transform;
	Vector3 moveDirection, fireDirection;
	public float fireDelay = 0.2f;
	public GameObject bullet;
	public float bulletSpeed = 10;
	Rigidbody _rigidbody;
	
	// Use this for initialization
	void Start () {
		_transform = transform;	
		_rigidbody = rigidbody;
		//StartCoroutine(FiringTimer());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
		_rigidbody.MovePosition(_transform.position + moveDirection * Time.deltaTime * moveSpeed);
			
	}
	
	IEnumerator FiringTimer(){
		while(true){
			if(Input.GetAxis("FireHorizontal") != 0.0f || Input.GetAxis("FireVertical") != 0.0f){
				Fire();		
				yield return new WaitForSeconds(fireDelay);
			} else{
				yield return null;
			}
			
		}
	}
	
	void OnCollisionEnter(){
		_rigidbody.velocity = Vector3.zero;
	}
	
	void Fire(){
		
		fireDirection = new Vector3(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"), 0).normalized;
		GameObject bulletInstance = Instantiate(bullet, _transform.position, Quaternion.LookRotation(fireDirection)) as GameObject;
		bulletInstance.rigidbody.AddForce(fireDirection * bulletSpeed,ForceMode.VelocityChange);
	}
}
