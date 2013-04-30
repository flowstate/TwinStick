using UnityEngine;
using System.Collections;

public class ShootForward : MonoBehaviour {
	
	public GameObject bullet;
	public float bulletSpeed = 5f;
	public float shootDelay = 1.5f;
	Transform _transform;
	Vector3 shootDirection;
	
	void Start () {
		_transform = transform;
		StartCoroutine(TimedShoot());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator TimedShoot(){
		while(true){
			Fire ();
			yield return new WaitForSeconds(shootDelay);
		}
	}
	
	void Fire(){
		// direction that object is facing in local Z direction
		shootDirection = _transform.forward;
		GameObject bulletInstance = Instantiate (bullet, _transform.position, Quaternion.LookRotation(shootDirection)) as GameObject;
		bulletInstance.rigidbody.AddForce(bulletSpeed*shootDirection, ForceMode.VelocityChange);
		Debug.Log("Firing Bullet");
		
	}
}
