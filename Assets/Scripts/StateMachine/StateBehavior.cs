using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all state machine behaviors. Allows for reflection in state switching.
/// </summary>
public class StateBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	#region Do Nothing Delegates
	public virtual void Log(){
		Debug.Log("I'm the default log!");
	}
	
	public virtual void DoUpdate(){
		if(Input.GetKeyDown(KeyCode.L))
		{
			Debug.Log("I'm the default Update behavior. What up?");
		}
	}
	public virtual void DoLateUpdate(){}
	public virtual void DoFixedUpdate(){}
	
	public virtual void DoOnMouseEnter(){}
	public virtual void DoOnMouseUp(){}
	public virtual void DoOnMouseDown(){}
	public virtual void DoOnMouseOver(){}
	public virtual void DoOnMouseExit(){}
	public virtual void DoOnMouseDrag(){}
	
	public virtual void DoOnCollisionEnter(Collision collision){}
	public virtual void DoOnCollisionExit(Collision collision){}
	public virtual void DoOnCollisionStay(Collision collision){}
	
	public virtual void DoOnTriggerEnter(Collider col){}
	public virtual void DoOnTriggerExit(Collider col){}
	public virtual void DoOnTriggerStay(Collider col){}
	
	#endregion
}
