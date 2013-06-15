using UnityEngine;
using System.Collections;

public class FiringCircle : MonoBehaviour
{

    public int ShotNum = 6;
    public int Offset = 0;
    public GameObject Projectile;
    public GameObject Special;
    public int SpecialPosition;
    public float StartRadius = 0;
    private float maxVel;
    private Transform _transform;

	// Use this for initialization
	void Start ()
	{
	    _transform = transform;
	    maxVel = Projectile.GetComponent<Bullet>().maxVelocity;
	}

    public void Fire(int mOffset)
    {
        GameObject tempShot;
        Vector3 startPos;

        for (int shotIndex = 0; shotIndex < ShotNum; shotIndex++)
        {
            int tempAngle = (shotIndex*(360/ShotNum)) + mOffset;

            Quaternion tempRotation = Quaternion.AngleAxis(tempAngle, Vector3.up);

            if (StartRadius == 0)
            {
                tempShot = Instantiate(Projectile, _transform.position, tempRotation) as GameObject;
            }
            else
            {
                //startPos = (_transform.position + (_transform.forward*StartRadius));
                startPos = _transform.position;
                startPos += tempRotation*(_transform.forward*StartRadius);
                tempShot = Instantiate(Projectile, startPos, tempRotation) as GameObject;
            }
            
            
            tempShot.rigidbody.AddForce(tempShot.transform.forward * maxVel, ForceMode.VelocityChange);
            
        }
    }

    

	// Update is called once per frame
	void Update () {
	
	}
}
