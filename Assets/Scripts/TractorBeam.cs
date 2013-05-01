using UnityEngine;
using System.Collections;

public class TractorBeam : MonoBehaviour {
	
	Transform _transform, captive;
	float foldTime = 1;
	bool isActive = true;
	bool doFling = false;
	// Use this for initialization
	void Start () {
		_transform = transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isActive){
			if(captive == null){
				isActive = true;
			}else{
				if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fling")){
				doFling = true;
				}
			}
		}
		else{
			
		}
	
	}
	
	// done in lateUpdate to track parent's movements during the Update call
	void LateUpdate(){
		if(captive != null){
			captive.localPosition = Vector3.zero;
			captive.localRotation = Quaternion.identity;
			
		}
	}
	
	void OnTriggerEnter(Collider col){
		if(isActive){
			col.transform.parent = _transform;
			captive = col.transform;
			captive.gameObject.layer = LayerMask.NameToLayer("PlayerBullets");
			isActive = false;
			//StartCoroutine(BringToTheFold(col.transform));
		}
		
	}
	
	void Fling(){
		captive.parent = null;
		captive.rigidbody.AddExplosionForce(1000,_transform.parent.transform.position,10);
		captive = null;
		isActive = true;
	}
	
	void FixedUpdate(){
		if(captive){
			if(doFling){
				Fling();
				doFling = false;
			}else{
				captive.rigidbody.velocity = Vector3.zero;
			}
			
		}
	}
	
	IEnumerator ActivateDelay(){
		yield return new WaitForSeconds(.5f);
		isActive = true;
	}
	
	IEnumerator BringToTheFold(Transform sheep){
		isActive = false;
		float elapsedTime = 0.0f;
		
		while(elapsedTime <= foldTime){
			elapsedTime += Time.deltaTime;
			// set position
			sheep.position = Vector3.Slerp(sheep.position, _transform.position, elapsedTime/foldTime);
			sheep.rotation = Quaternion.Slerp(sheep.rotation, _transform.rotation, elapsedTime/foldTime);

			yield return null;
		}
		
	}
	
}
