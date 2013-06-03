using UnityEngine;
using System.Collections;

public class SmoothFollowTarget : MonoBehaviour {

    private Transform _transform, _targetTransform;

    public GameObject target;
    private Vector3 positionOffset;
    public float deltaTime = 1.0f;

	// Use this for initialization
	void Start () {
        _transform = transform;
        _targetTransform = target.transform;
        positionOffset = _transform.position - _targetTransform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null) {
            Vector3 desiredPosition = _targetTransform.position + positionOffset;
            _transform.position = Vector3.Lerp(_transform.position, desiredPosition, deltaTime);
        }

	}
}
