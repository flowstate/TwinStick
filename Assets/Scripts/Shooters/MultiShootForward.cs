using UnityEngine;
using System.Collections;

public class MultiShootForward : ShootForward
{

    public int BulletNum = 3;
    public int SpreadAngleTotal = 45;
    private int _angleBetweenShots;
    private GameObject tempShot;
    private Quaternion tempRotation;

	// Use this for initialization
	void Start ()
	{
	    _angleBetweenShots = SpreadAngleTotal/BulletNum;
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Fire()
    {
        Debug.Log("Calling the new one.");
        float halfArray = Mathf.Floor(BulletNum/2);
        for (int shotIndex = 0; shotIndex < BulletNum; shotIndex++)
        {
            float tempAngle = (shotIndex - halfArray)*_angleBetweenShots;
            tempRotation = Quaternion.AngleAxis(tempAngle, Vector3.up);

            tempShot = Instantiate(bullet, _transform.position, tempRotation) as GameObject;
            tempShot.rigidbody.AddForce(bulletSpeed * (tempRotation * _transform.forward), ForceMode.VelocityChange);

        }
    }
}
