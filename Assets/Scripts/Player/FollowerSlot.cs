using UnityEngine;
using System.Collections;

public class FollowerSlot : MonoBehaviour
{
    public GameObject Leader, RotationMatch;
    public float Offset;
    private float sqrOffset;
    private Transform _transform, targetTransform, rotationTransform;
    public Vector3 VectorOffset;
    public float TransitionTime;
    private int currentOffset = 1;
    private Hashtable bounceTable;
    private Vector3 vectorOffset, desiredPosition;
    public KeyCode SwitchPosition;
    private bool doLerp = true;
    public int CurrentPosition;
    private GameObject _filler = null;

    public GameObject Filler
    {
        get { return _filler; }
        set { GetFiller(value); }
    }

    

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

    private void GetFiller(GameObject gObject)
    {
        if (_filler == null)
        {
            _filler = gObject;
            _filler.transform.parent = _transform;
            _filler.transform.position = _transform.position;
            _filler.transform.rotation = _transform.rotation;
        }
        else
        {
            Debug.Log("Dont triple-stamp a double-stamp");
        }

    }

    private void FillerGone()
    {
        SendMessageUpwards("FillerGone", CurrentPosition, SendMessageOptions.RequireReceiver);
        _filler = null;
    }

    public void PopFiller()
    {
        _filler = null;
    }


	private void CalculatePosition()
	{
	    desiredPosition = targetTransform.TransformPoint(VectorOffset);
        
	}

	// Update is called once per frame
	void Update ()
	{

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
