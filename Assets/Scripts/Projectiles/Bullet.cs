using UnityEngine;
using System.Collections;


public class Bullet : MonoBehaviour {
	
	
	protected Transform _transform;
	protected Rigidbody _rigidbody;
	public GameObject target;
	public float maxVelocity = 3;
	public bool findPlayer = false;
    public LayerMask CollisionMask;
	
	protected virtual void Start(){
		InitCached();
		if(findPlayer){
			target = GameObject.FindWithTag("Player"); 
		}
		
	}
	
	protected void InitCached(){
		_rigidbody = rigidbody;
		_transform = transform;
	}
	
	protected virtual void FixedUpdate(){

	}
	
	protected virtual void OnCollisionEnter(Collision collision){
        if (Constants.IsInLayerMask(collision.gameObject, CollisionMask))
        {
            Destroy(gameObject);
        }
	}
	
	
	
}
