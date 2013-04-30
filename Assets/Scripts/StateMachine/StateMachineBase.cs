using UnityEngine;
using System.Collections;
using System;

public class StateMachineBase : MonoBehaviour {
	
	public new Transform transform;
	public new Rigidbody rigidBody;
	public new Animation animation;
	public CharacterController controller; // may not be needed
	public new Collider collider;
	public new GameObject gameObject;
	
	void Awake(){
		gameObject = base.gameObject;
		rigidBody = base.rigidbody;
		collider = base.collider;
		transform = base.transform;
		animation = base.animation;
		OnAwake();
	}
	
	protected virtual void OnAwake(){
		
	}
	
	static IEnumerator DoNothingCoroutine(){
		yield break;
	}
	
	static void DoNothing(){
		
	}
	
	static void DoNothingCollider(Collider other){
		
	}
	
	static void DoNothingCollision(Collision other){
		
	}
	
	public Action DoUpdate = DoNothing;
	public Action DoLateUpdate = DoNothing;
	public Action DoFixedUpdate = DoNothing;
	public Action<Collider> DoOnTriggerEnter = DoNothingCollider;
	public Action<Collider> DoOnTriggerStay = DoNothingCollider;
	public Action<Collider> DoOnTriggerExit = DoNothingCollider;
	public Action<Collision> DoOnCollisionEnter = DoNothingCollision;
	public Action<Collision> DoOnCollisionStay = DoNothingCollision;
	public Action<Collision> DoOnCollisionExit = DoNothingCollision;
	public Action DoOnMouseEnter = DoNothing;
	public Action DoOnMouseUp = DoNothing;
	public Action DoOnMouseDown = DoNothing;
	public Action DoOnMouseOver = DoNothing;
	public Action DoOnMouseExit = DoNothing;
	public Action DoOnMouseDrag = DoNothing;
	public Action DoOnGUI = DoNothing;
	public Func<IEnumerator> ExitState = DoNothingCoroutine;
	
	private Enum _currentState;
	
	public Enum currentState{
		get{
			return _currentState;
		}
		
		set{
			_currentState = value;
			ConfigureCurrentState();
		}
	}
	
	void ConfigureCurrentState(){
		if(ExitState != null){
			StartCoroutine(ExitState());
		}
		
		// configure all the state methods
		DoUpdate = ConfigureDelegate<Action>("Update", DoNothing);
    	DoOnGUI = ConfigureDelegate<Action>("OnGUI", DoNothing);
    	DoLateUpdate = ConfigureDelegate<Action>("LateUpdate", DoNothing);
    	DoFixedUpdate = ConfigureDelegate<Action>("FixedUpdate", DoNothing);
    	DoOnMouseUp = ConfigureDelegate<Action>("OnMouseUp", DoNothing);
    	DoOnMouseDown = ConfigureDelegate<Action>("OnMouseDown", DoNothing);
    	DoOnMouseEnter = ConfigureDelegate<Action>("OnMouseEnter", DoNothing);
    	DoOnMouseExit = ConfigureDelegate<Action>("OnMouseExit", DoNothing);
    	DoOnMouseDrag = ConfigureDelegate<Action>("OnMouseDrag", DoNothing);
    	DoOnMouseOver = ConfigureDelegate<Action>("OnMouseOver", DoNothing);
    	DoOnTriggerEnter = ConfigureDelegate<Action<Collider>>("OnTriggerEnter", DoNothingCollider);
    	DoOnTriggerExit = ConfigureDelegate<Action<Collider>>("OnTriggerExir", DoNothingCollider);
    	DoOnTriggerStay = ConfigureDelegate<Action<Collider>>("OnTriggerEnter", DoNothingCollider);
    	DoOnCollisionEnter = ConfigureDelegate<Action<Collision>>("OnCollisionEnter", DoNothingCollision);
    	DoOnCollisionExit = ConfigureDelegate<Action<Collision>>("OnCollisionExit", DoNothingCollision);
    	DoOnCollisionStay = ConfigureDelegate<Action<Collision>>("OnCollisionStay", DoNothingCollision);
    	Func<IEnumerator> enterState = ConfigureDelegate<Func<IEnumerator>>("EnterState", DoNothingCoroutine);
    	ExitState = ConfigureDelegate<Func<IEnumerator>>("ExitState", DoNothingCoroutine);
    
		//Optimisation, turn off GUI if we don't
        //have an OnGUI method
    	// EnableGUI();
        
		//Start the current state
    	StartCoroutine(enterState());
	}
	
	T ConfigureDelegate<T>(string methodRoot, T Default) where T: class {
		
		var method = GetType().GetMethod(_currentState.ToString() + "_" + methodRoot, 
			System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public);
		
		// if we found a method
		if(method != null){
			// Create a delegate of the type that this generic instance needs
			// and cast it
			return Delegate.CreateDelegate(typeof(T), this, method) as T;
		}else{
			return Default;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		DoUpdate();
	}
}
