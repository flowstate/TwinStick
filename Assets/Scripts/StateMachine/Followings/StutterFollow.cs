using UnityEngine;
using System.Collections;

public class StutterFollow : FollowingBehavior
{



    public float speed = 1;
    public float rotateSpeed = 5f;
    public float maxAcceleration = 1;
    public float maxVelocity = 3;
    public GameObject target;
    TwinStickShipZed targetShip;
    Transform targetTransform = null;
    int amount = 11;
    Transform _transform;
    Rigidbody _rigidbody;
    Vector3 aimVector;

    public float AngleDelta = 45f;
    public float PauseTime = 1.0f;
    public float TravelDistance = 2.0f;
    public float TravelTime = 0.5f;

    private Hashtable moveTable;
    public override void DoAwake()
    {
        target = owner.target;
        targetTransform = target.transform;
        _transform = transform;
        _rigidbody = rigidbody;
        targetShip = target.GetComponent<TwinStickShipZed>();

        InitTable();

    }

    private void InitTable()
    {
        moveTable = new Hashtable();

        moveTable.Add("name", "moveTween");
        moveTable.Add("lookTarget", target.transform);
        moveTable.Add("looktime", 0.2f);
        moveTable.Add("amount", aimVector);
        Debug.Log("Original hashVector: " + moveTable["amount"].ToString());
        moveTable.Add("time", TravelTime);
        moveTable.Add("oncomplete", "MoveComplete");
        moveTable.Add("delay", PauseTime);
        moveTable.Add("space", Space.World);
        GenerateMovement();
        iTween.MoveBy(gameObject, moveTable);

    }

    private void GenerateMovement()
    {

        float randomAngle = Random.Range(-AngleDelta, AngleDelta);
        float tempDistance = Random.Range(TravelDistance - 1.0f, TravelDistance + 2.0f);
        float tempTime = Random.Range(TravelTime - 0.2f, TravelTime + 0.2f);
        float tempPause = Random.Range(PauseTime - 0.5f, PauseTime + 1f);

        // initial direction
        aimVector = target.transform.position - _transform.position;

        // multiply by random angle within range
        aimVector = Quaternion.AngleAxis(randomAngle, Vector3.up) * aimVector;

        aimVector = aimVector.normalized*TravelDistance;

        Debug.Log("New hashVector: " + moveTable["amount"].ToString());
        // set moveTable
        moveTable["amount"] = aimVector;
        moveTable["time"] = tempTime;

        // randomize speed

    }

    private void MoveComplete()
    {
        GenerateMovement();
        iTween.MoveBy(gameObject, moveTable);
    }

    public override void DoUpdate()
    {
        RotateTowardsTarget();
    }

    public static IEnumerator EnterState()
    {
        Debug.Log("Entering Stutter");

        yield return null;
    }

    public static IEnumerator ExitState()
    {
        Debug.Log("Exiting Stutter");
        yield return null;
    }

    public override void DoFixedUpdate()
    {
        //if (target != null)
        //{

        //    if (targetShip.hasCaptured)
        //    {
        //        FleeTight();
        //    }
        //    else
        //    {
        //        SeekTight();
        //    }


        //}
    }

    public void RotateTowardsTarget()
    {
        if (target != null)
        {
            // calculate aim vector as the way we would be pointing to be looking at the player
            aimVector = targetTransform.position - _transform.position;
            //_transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(_transform.forward, targetTransform.position - _transform.position, rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime),_transform.up);


            _transform.rotation = Quaternion.RotateTowards(
                // Rotate from current rotation (lookRotation) 
                Quaternion.LookRotation(_transform.forward, _transform.up),
                // Towards the rotation we would need to be looking at the player
                Quaternion.LookRotation(aimVector, _transform.up),
                // At a rate of rotateSpeed (radians) * radian to degree ratio * elapsed time
                rotateSpeed * Mathf.Rad2Deg * Time.deltaTime
                );
        }
    }

    //public void SeekTight()
    //{
    //    Vector3 desiredDirection = (targetTransform.position - _transform.position).normalized * maxVelocity * maxVelocity;

    //    _rigidbody.AddForce(_rigidbody.velocity + desiredDirection);
    //    _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
    //}

    //public void FleeTight()
    //{
    //    Vector3 desiredDirection = (_transform.position - targetTransform.position).normalized * maxVelocity * maxVelocity;

    //    rigidbody.AddForce(rigidbody.velocity + desiredDirection);
    //    rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
    //}

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            target = newTarget;
            targetTransform = target.transform;
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }

    }
}
