using UnityEngine;
using System.Collections;

public class SeekBullet : Bullet {
	
	public float awareRadius = 3f;
	public LayerMask hitMask;
	
	// Use this for initialization
	protected override void Start () {
		InitCached();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected override void FixedUpdate ()
	{
		SeekTarget();
	}
	
	void SeekTarget(){
		
		if(target == null){
			FindTargetsInRange();
		}else{
			Vector3 desiredDirection = (target.transform.position - _transform.position).normalized * maxVelocity *maxVelocity;
								
			_rigidbody.AddForce(_rigidbody.velocity + desiredDirection);
			_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
		}
		
			
	}
	
	void FindTargetsInRange(){
		Collider[] hitColliders = Physics.OverlapSphere(_transform.position, awareRadius, hitMask);
		
		for(int i = 0; i < hitColliders.Length; i++){
			if(hitColliders[i].gameObject.CompareTag("Player")){
				target = hitColliders[i].gameObject;
			}
		}
	}
	
	protected override void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag.Equals("Player")){
			return;	
		}else{
			base.OnCollisionEnter(collision);
		}
	}
}
