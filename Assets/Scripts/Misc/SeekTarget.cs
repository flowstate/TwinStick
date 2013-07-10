using UnityEngine;
using System.Collections;

public class SeekTarget : MonoBehaviour
{

    public LayerMask TargetLayers;
    private GameObject target = null;
    public float RotateSpeed = 5f;
    public float MaxAcceleration = 1;
    public float MaxVelocity = 3;
    private float maxVelSqr;
    private Vector3 desiredDirection;

    private Transform _transform, targetTransform;
    private Rigidbody _rigidbody;

    // Use this for initialization
	void Start ()
	{

	    _transform = transform;
	    _rigidbody = rigidbody;
	    maxVelSqr = MaxVelocity*MaxVelocity;
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null)
        {
            SeekTight();
        }

	}

    private void SeekTight()
    {
        desiredDirection = (targetTransform.position - _transform.position).normalized*maxVelSqr;

        _rigidbody.AddForce(_rigidbody.velocity + desiredDirection);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, MaxVelocity);

    }

    public void SetTarget(GameObject newTarget)
    {
        if (target == null)
        {
            target = newTarget;
            targetTransform = newTarget.transform;
        }
    }

    
}
