using UnityEngine;
using System.Collections;

public class RotateUnitCircle : MonoBehaviour {
	
	Transform _transform;
	Vector3 originalPosition;
	float distance = 2f;
	// Use this for initialization
	void Start () {
		_transform = transform;
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		// get axes
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");
		
		Vector3 newPosition = new Vector3(xAxis, 0, yAxis).normalized;
		
		transform.position = newPosition;
	}
}
