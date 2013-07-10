using UnityEngine;
using System.Collections;

public class TestWallBounce : MonoBehaviour {

    public Vector3 MoveDirection = new Vector3(1, 0, 0);

    public bool RandomizeStart = false;
    public float MaxSpeed = 10f;
    public LayerMask ChangeDirectionMask;
    private bool flipX = false, flipZ = false;
    private bool setMotion = true;
    private Rigidbody _rigidbody;



	// Use this for initialization
	void Start ()
	{
	    _rigidbody = rigidbody;
        if (RandomizeStart)
        {
            RandomizeDirection();
        }
        setMotion = true;
	
	}

    public void FixedUpdate()
    {
        if (setMotion = true)
        {
            InitiateMotion();
        }
    }

    private void RandomizeDirection()
    {
        float newX, newZ;
        bool posX, posZ;

        if (MoveDirection.x > 0)
        {
            posX = !flipX;
        }
        else
        {
            posX = flipX;
        }

        if (MoveDirection.z > 0)
        {
            posZ = !flipZ;
        }
        else
        {
            posZ = flipZ;
        }

        newX = posX ? Random.Range(0, 1) : Random.Range(-1, 0);
        newZ = posZ ? Random.Range(0, 1) : Random.Range(-1, 0);
        MoveDirection = new Vector3(newX, 0, newZ);
        Debug.Log("New direction: " + MoveDirection.ToString("0.00"));
        flipX = flipZ = false;
        setMotion = true;
    }


    private void InitiateMotion()
    {
       // _rigidbody.velocity = MoveDirection.normalized * MaxSpeed;
        _rigidbody.AddForce(MoveDirection.normalized * MaxSpeed, ForceMode.VelocityChange);
        setMotion = false;


    }

    public void OnCollisionEnter(Collision collision)
    {
        if (Constants.IsInLayerMask(collision.gameObject, ChangeDirectionMask))
        {

            Vector3 point = transform.InverseTransformDirection(collision.contacts[0].point);


            // if the wall is on the right and we're moving to the right
            if (point.x > 0)
            {
                Debug.Log("Point is to the right");
                if (MoveDirection.x > 0)
                {
                    Debug.Log("Flipping X");
                    flipX = true;
                }
            }
            else if (MoveDirection.x < 0)
            {
                Debug.Log("X was left, flipping");
                flipX = true;
            }

            if (point.z > 0)
            {
                Debug.Log("Point is above");
                if (MoveDirection.z > 0)
                {
                    Debug.Log("Flipping Z");
                    flipZ = true;
                }
            }

            else if (MoveDirection.z < 0)
            {
                Debug.Log("Were moving down, flipping Z");
                flipZ = true;
            }

            RandomizeDirection();

            Debug.Log("Collision normal: " + collision.contacts[0].normal.ToString("0.00"));
        }


    }

}
