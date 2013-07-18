using UnityEngine;
using System.Collections;

public class FollowerSlot : MonoBehaviour
{
    public GameObject Leader;
    public float Offset;
    private float sqrOffset;
    private Transform _transform, targetTransform;
    private Vector3 vectorOffset, desiredPosition;
    public KeyCode outputKey;

    // Use this for initialization
	void Start ()
	{
        vectorOffset = new Vector3(0,0,-2);

	    sqrOffset = Offset*Offset;
	    _transform = transform;
	    targetTransform = Leader.transform;

	}
    
	private void CalculatePosition()
	{
        Debug.Log("Leader world position: " + targetTransform.position.ToString("0.00"));
        Debug.Log("Leader local position: " + targetTransform.InverseTransformDirection(targetTransform.position).ToString("0.00"));
        
        Debug.Log("Desired position 1: " + targetTransform.TransformPoint(vectorOffset));

	    
	}

	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(outputKey))
        {
            CalculatePosition();
        }

        if (ComeCloser())
        {
            Move();
        }
	}

    private void Move()
    {
        iTween.MoveUpdate(gameObject, targetTransform.position, 1.5f);
    }

    private bool ComeCloser()
    {

        return (targetTransform.position - _transform.position).sqrMagnitude > sqrOffset;
    }

    
}
