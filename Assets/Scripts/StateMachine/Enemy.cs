using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

public enum EnemyStates{
	PATROL,
	FOLLOWING,
	ATTACK,
}

public class Enemy : MonoBehaviour {
	
		
	/// <summary>
	/// Class that represents the settings for a particular state
	/// </summary>
	public class State
	{
		public String stateName;
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
		public Func<IEnumerator> enterState = DoNothingCoroutine;
		public Func<IEnumerator> exitState = DoNothingCoroutine;
		public IEnumerator enterStateEnumerator = null;
		public IEnumerator exitStateEnumerator = null;
		
		public State(StateBehavior behavior){
			DoUpdate = (Action)Delegate.CreateDelegate(DoUpdate.GetType(),behavior,"DoUpdate");
			DoLateUpdate = (Action)Delegate.CreateDelegate(DoLateUpdate.GetType(),behavior,"DoLateUpdate");
			DoFixedUpdate = (Action)Delegate.CreateDelegate(DoFixedUpdate.GetType(),behavior, "DoFixedUpdate");
			
			DoOnTriggerEnter = (Action<Collider>)Delegate.CreateDelegate(DoOnTriggerEnter.GetType(),behavior, "DoOnTriggerEnter");
			DoOnTriggerExit = (Action<Collider>)Delegate.CreateDelegate(DoOnTriggerExit.GetType(),behavior, "DoOnTriggerExit");
			DoOnTriggerStay = (Action<Collider>)Delegate.CreateDelegate(DoOnTriggerStay.GetType(),behavior, "DoOnTriggerStay");
			
			DoOnCollisionEnter = (Action<Collision>)Delegate.CreateDelegate(DoOnCollisionEnter.GetType(),behavior, "DoOnCollisionEnter");
			DoOnCollisionExit = (Action<Collision>)Delegate.CreateDelegate(DoOnCollisionExit.GetType(),behavior, "DoOnCollisionExit");
			DoOnCollisionStay = (Action<Collision>)Delegate.CreateDelegate(DoOnCollisionStay.GetType(),behavior, "DoOnCollisionStay");
			
			DoOnMouseEnter = (Action)Delegate.CreateDelegate(DoOnMouseEnter.GetType(),behavior, "DoOnMouseEnter");
			DoOnMouseUp = (Action)Delegate.CreateDelegate(DoOnMouseEnter.GetType(),behavior, "DoOnMouseUp");
			DoOnMouseDown = (Action)Delegate.CreateDelegate(DoOnMouseEnter.GetType(),behavior, "DoOnMouseDown");
			DoOnMouseOver = (Action)Delegate.CreateDelegate(DoOnMouseEnter.GetType(),behavior, "DoOnMouseOver");
			DoOnMouseExit = (Action)Delegate.CreateDelegate(DoOnMouseEnter.GetType(),behavior, "DoOnMouseExit");
			DoOnMouseDrag = (Action)Delegate.CreateDelegate(DoOnMouseEnter.GetType(),behavior, "DoOnMouseDrag");
		}
		
		/*
		
		public object currentState;
		//Stack of the enter state enumerators
		public Stack<IEnumerator> enterStack;
		//Stack of the exit state enumerators
		public Stack<IEnumerator> exitStack;
		//The amount of time that was spend in this state
		//when pushed to the stack
		public float time;
		
		public StateMachineBehaviourEx executingStateMachine; */
	}
	
	
	#region Delegate Members
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
	public Action DoLog = BasicLog;
	public Func<IEnumerator> ExitState = DoNothingCoroutine;
	#endregion
	
	List<State> stateList = new List<State>();
	[HideInInspector]
	public State state
	{
		get{return stateList[(int)currentState];}
	}
	
	Transform _transform;
	[HideInInspector]
	public AttackBehavior _attack;
	
	[HideInInspector]
	public FollowingBehavior _following;
	
	[HideInInspector]
	public PatrolBehavior _patrol;
	
	EnemyStates _currentState;
	public EnemyStates currentState
	{	
		get{return _currentState;}
		set{SwitchState(value);}
	}
	
	StateBehavior currentBehavior;
	// Use this for initialization
	void Start () {
		_transform = transform;
		_attack = _transform.GetComponent<AttackBehavior>();
		_patrol = _transform.GetComponent<PatrolBehavior>();
		_following = _transform.GetComponent<FollowingBehavior>();
		InitializeStates();
		currentState = EnemyStates.PATROL;
	}
	
	void InitializeStates(){
		// for each item in enum
		foreach(EnemyStates eState in Enum.GetValues(typeof(EnemyStates)))
		{
			
			Debug.Log(eState.ToString());
			
			// reflect on the behaviour
			String bName = "_" + eState.ToString().ToLower();
			Debug.Log(bName);
			FieldInfo bField = GetType().GetField(bName);
			if(bField == null)
			{
				Debug.Log("No behavior called " + bName );
				
				// wire in base null behavior
			}
			else
			{
				StateBehavior tempBehavior = (StateBehavior)bField.GetValue(this);
				if(tempBehavior == null)
				{
					Debug.Log("Null in else");
				}
				else
				{
					State tempState = new State(tempBehavior);
					stateList.Add(tempState);
				}
				
			}
			
		}
		
		Debug.Log("Number of states in list: " + stateList.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.A)){
			IncrementState();
			Debug.Log("Current State: " + currentState.ToString());
		}
		state.DoUpdate();
		
		
	}
	
	public void SwitchState(EnemyStates newState){
		Debug.Log("Executing SwitchState, properties work!");
		if(currentState == newState){
			Debug.Log("CurrentState == newState");
			return;
		}
		
		// execute exit for current state
		Debug.Log("Exiting current state: " + currentState.ToString());
		// discover and set up new state
		_currentState = newState;
		
		// execute enter for new state
		Debug.Log("Entering new state: " + currentState.ToString());
	}
	
	void ConfigureState(){
		
		
	}
	
	void IncrementState(){
		if(currentState == EnemyStates.PATROL)
			currentState = EnemyStates.ATTACK;
		else
			currentState = EnemyStates.PATROL;
	}
	
	void FixedUpdate(){
		
	}
	
	void LateUpdate(){
		
	}
	
	#region Default Implementations Of Delegates
	
	static IEnumerator DoNothingCoroutine()
	{
		yield break;
	}
	
	static void DoNothing()
	{
	}
	
	static void DoNothingCollider(Collider other)
	{
	}
	
	static void DoNothingCollision(Collision other)
	{
	}
	
	static void BasicLog(){
		Debug.Log("I'm a basic log!!");
	}
	
	#endregion

}
