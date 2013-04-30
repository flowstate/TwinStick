using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	public float speed = 5;
	public GameObject target;
	Transform targetTransform = null;
	// Use this for initialization
	void Start () {
		SetTarget(target);
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null){
			Vector3 moveDirection = (targetTransform.position - transform.position).normalized;
			transform.position += moveDirection * Time.deltaTime * speed;
		}
		
	}
	
	public void SetTarget(GameObject newTarget){
		if(newTarget != null){
			target = newTarget;
			targetTransform = target.transform;
		}
		
	}
}
