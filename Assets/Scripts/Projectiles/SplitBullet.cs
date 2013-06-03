using UnityEngine;
using System.Collections;

public class SplitBullet : Bullet {
	
	public int splitNum = 4;
	public int offset = 0;
	public GameObject children;
	// Use this for initialization
	protected override void Start () {
		InitCached();
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.L)){
			Split();
		}
	
	}
	
	void Split(){
		if(children != null){
			
			Vector3 offsetSpawn = new Vector3(0,3,0);
			
			// get splitNum angles
			for(int childIndex = 0; childIndex < splitNum; childIndex++){
				// get angle
				int tempAngle = (childIndex * (360 / splitNum)) + offset;
				
				// get rotation
				Quaternion tempRotation = Quaternion.AngleAxis(tempAngle, Vector3.up);
				
				// set rotation of child
				GameObject tempChild = Instantiate(children,_transform.position, tempRotation) as GameObject;
				
				// add force
				tempChild.rigidbody.AddForce(tempChild.transform.forward * maxVelocity, ForceMode.VelocityChange);
			}
			
			Destroy(gameObject);
			
		}else{
			Debug.Log("No children set for splitter");
		}
	}
}
