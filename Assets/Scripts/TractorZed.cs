using UnityEngine;
using System.Collections;

public class TractorZed : MonoBehaviour {
	
	public float rotateSpeed = 60.0f;
	Transform _transform;
	public LayerMask mask;
	Transform _beamTransform;
	public float minDistance = 2,  
		maxDistance = 15;
	float minDistanceSquared, maxDistanceSquared, mouseDistanceSquared;
	Vector3 areaLocation;
	// Use this for initialization
	void Start () {
		_transform = transform;
		_beamTransform = _transform.Find("TractorArea");
		minDistanceSquared = minDistance * minDistance;
		maxDistanceSquared = maxDistance * maxDistance;
		
	}
	
	// Update is called once per frame
	void Update () {
		// _transform.Rotate(0.0f, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed, 0.0f);
		SetRotation();
	}
	
	void SetRotation(){
		
		if(Input.GetAxis("FireHorizontal") != 0 || Input.GetAxis("FireVertical") != 0){
			Vector3 targetRotVector = new Vector3(Input.GetAxis("FireHorizontal"), 0, Input.GetAxis("FireVertical")).normalized;
			Quaternion targetRotation = Quaternion.LookRotation(targetRotVector);
			_transform.rotation = Quaternion.Slerp(_transform.rotation,targetRotation, 1); 
		}
		
		
		/*Plane playerPlane = new Plane(Vector3.up, _transform.position);
		Ray hitRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		float hitDist = 0.0f;
		
		if(playerPlane.Raycast(hitRay,out hitDist))
		{
			Vector3 targetPoint = hitRay.GetPoint(hitDist);
			
			Vector3 targetRotVector = new Vector3(targetPoint.x, 0, targetPoint.z);
			//Vector3 targetRotVector = new Vector3(targetPoint.x, 0, targetPoint.z);
			Quaternion targetRotation = Quaternion.LookRotation((targetRotVector - _transform.position).normalized);
			_transform.rotation = Quaternion.Slerp(_transform.rotation,targetRotation, 1); 
			//SetAreaLocation(targetPoint);
		}*/
	}
	
	void SetAreaLocation(Vector3 tPoint){
		Vector3 distanceFromCurrent = tPoint - _transform.position;
		
		mouseDistanceSquared = (tPoint - _transform.position).sqrMagnitude;
		float clampedLocation = Mathf.Clamp(mouseDistanceSquared, minDistanceSquared, maxDistanceSquared);
		Debug.Log("Zed distance: " + distanceFromCurrent.z);
		//_beamTransform.position = new Vector3(_beamTransform.position.x, _beamTransform.position.y, Mathf.Sqrt(clampedLocation));
		_beamTransform.position = new Vector3(_beamTransform.position.x, _beamTransform.position.y, distanceFromCurrent.z);
		// too close
		if((tPoint - _transform.position).sqrMagnitude < minDistanceSquared){
			//_beamTransform.position = tPoint.normalized * minDistance;
			// _beamTransform.position = new Vector3(_beamTransform.position.x, _beamTransform.y, clampedLocation);
		}
		else{
			// _beamTransform.position = Vector3.ClampMagnitude(tPoint,5.0f);
		}
		
	}
}
