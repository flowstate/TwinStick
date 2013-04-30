using UnityEngine;
using System.Collections;

public class AttackBehavior : StateBehavior {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void Log(){
		Debug.Log("Attack behavior log!");
	}
	
	public override void DoUpdate(){
		if(Input.GetKeyDown(KeyCode.L))
		{
			Debug.Log("I'm the ATTACK Update behavior. What up?");
		}
	}
}
