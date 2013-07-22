using UnityEngine;
using System.Collections;

public class FollowerSlot : MonoBehaviour
{
    public GameObject Leader, RotationMatch;
    public float Offset;
    private float sqrOffset;
    private Transform _transform, targetTransform, rotationTransform;
    public Vector3 VectorOffset, VectorOffsetTwo, VectorOffsetThree;
    public float TransitionTime;
    private int currentOffset = 1;
    private Hashtable bounceTable;
    private Vector3 vectorOffset, desiredPosition;
    public KeyCode SwitchPosition;
    private bool doLerp = true;
    // Use this for initialization
	void Start ()
	{
	    vectorOffset = VectorOffset;
        bounceTable = new Hashtable();
        bounceTable.Add("y", 4f);
        bounceTable.Add("time", 1);
        bounceTable.Add("oncomplete", "NoBounceForYou");
        bounceTable.Add("oncompletetarget", gameObject);

	    sqrOffset = Offset*Offset;
	    _transform = transform;
	    targetTransform = Leader.transform;
	    rotationTransform = RotationMatch.transform;

	}
    
    public void NextPattern()
    {
        switch (currentOffset)
        {
            case 1:

                //currentOffset = 2;
                vectorOffset = VectorOffsetTwo;
                break;
            case 2:
                //currentOffset = 3;
                vectorOffset = VectorOffsetThree;
                break;
            case 3:
                //currentOffset = 1;
                vectorOffset = VectorOffset;
                break;
            default:
                break;
        }

        currentOffset = (currentOffset%3) + 1;
    }

	private void CalculatePosition()
	{
	    desiredPosition = targetTransform.TransformPoint(vectorOffset);
        
	}

	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.B))
        {
            Bounce();
        }

        if (Input.GetKeyDown(SwitchPosition) || Input.GetButtonDown("Formation") )
        {
            NextPattern();
        }

        Rotate();

        if (ComeCloser())
        {
            Move();
            
        }
	}

    private void Bounce()
    {
        doLerp = false;
        iTween.PunchPosition(gameObject, bounceTable);
    }

    public void NoBounceForYou()
    {
        doLerp = true;
    }

    private void Rotate()
    {
        _transform.rotation = rotationTransform.rotation;
    }

    private void Move()
    {
        if (doLerp)
        {
            _transform.position = Vector3.Lerp(_transform.position, desiredPosition, TransitionTime);    
        }
        
    }

    private bool ComeCloser()
    {
        CalculatePosition();

        return (desiredPosition - _transform.position).sqrMagnitude > 0.1f;
    }

    
}
