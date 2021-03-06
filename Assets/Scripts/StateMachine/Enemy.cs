using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

public enum EnemyStates{
	PATROL,
	FOLLOWING,
	ATTACK,
    CAPTURED,
    FLUNG
}

public class Enemy : MonoBehaviour {
	
		
	/// <summary>
	/// Class that represents the settings for a particular state
	/// </summary>
	public class State
    {
        #region
        public String stateName;
        public Action DoAwake = DoNothing;
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
	    public Action DoExit = DoNothing;
	    public Action DoEnter = DoNothing;
        public Action DoOnGUI = DoNothing;
        public Func<IEnumerator> enterState = DoNothingCoroutine;
        public Func<IEnumerator> exitState = DoNothingCoroutine;
        public IEnumerator enterStateEnumerator = null;
        public IEnumerator exitStateEnumerator = null;
        #endregion
        
		
		public State(StateBehavior behavior){
            try
            {
                DoAwake = (Action) Delegate.CreateDelegate(DoUpdate.GetType(), behavior, "DoAwake");
                DoUpdate = (Action) Delegate.CreateDelegate(DoUpdate.GetType(), behavior, "DoUpdate");
                DoLateUpdate = (Action) Delegate.CreateDelegate(DoLateUpdate.GetType(), behavior, "DoLateUpdate");
                DoFixedUpdate = (Action) Delegate.CreateDelegate(DoFixedUpdate.GetType(), behavior, "DoFixedUpdate");
                DoEnter = (Action)Delegate.CreateDelegate(DoUpdate.GetType(), behavior, "DoEnter");
                DoExit = (Action)Delegate.CreateDelegate(DoUpdate.GetType(), behavior, "DoExit");
                DoOnTriggerEnter =
                    (Action<Collider>) Delegate.CreateDelegate(DoOnTriggerEnter.GetType(), behavior, "DoOnTriggerEnter");
                DoOnTriggerExit =
                    (Action<Collider>) Delegate.CreateDelegate(DoOnTriggerExit.GetType(), behavior, "DoOnTriggerExit");
                DoOnTriggerStay =
                    (Action<Collider>) Delegate.CreateDelegate(DoOnTriggerStay.GetType(), behavior, "DoOnTriggerStay");

                DoOnCollisionEnter =
                    (Action<Collision>)
                    Delegate.CreateDelegate(DoOnCollisionEnter.GetType(), behavior, "DoOnCollisionEnter");
                DoOnCollisionExit =
                    (Action<Collision>)
                    Delegate.CreateDelegate(DoOnCollisionExit.GetType(), behavior, "DoOnCollisionExit");
                DoOnCollisionStay =
                    (Action<Collision>)
                    Delegate.CreateDelegate(DoOnCollisionStay.GetType(), behavior, "DoOnCollisionStay");

                DoOnMouseEnter = (Action) Delegate.CreateDelegate(DoOnMouseEnter.GetType(), behavior, "DoOnMouseEnter");
                DoOnMouseUp = (Action) Delegate.CreateDelegate(DoOnMouseEnter.GetType(), behavior, "DoOnMouseUp");
                DoOnMouseDown = (Action) Delegate.CreateDelegate(DoOnMouseEnter.GetType(), behavior, "DoOnMouseDown");
                DoOnMouseOver = (Action) Delegate.CreateDelegate(DoOnMouseEnter.GetType(), behavior, "DoOnMouseOver");
                DoOnMouseExit = (Action) Delegate.CreateDelegate(DoOnMouseEnter.GetType(), behavior, "DoOnMouseExit");
                DoOnMouseDrag = (Action) Delegate.CreateDelegate(DoOnMouseEnter.GetType(), behavior, "DoOnMouseDrag");

                MethodInfo[] infos =
                    behavior.GetType()
                            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                        BindingFlags.InvokeMethod | BindingFlags.Static);



                MethodInfo enterMethod = behavior.GetType()
                                                 .GetMethod("EnterState", System.Reflection.BindingFlags.Instance
                                                                          | System.Reflection.BindingFlags.Public |
                                                                          System.Reflection.BindingFlags.NonPublic |
                                                                          System.Reflection.BindingFlags.InvokeMethod |
                                                                          System.Reflection.BindingFlags.Static);

                if (enterMethod != null)
                {
                    enterState = (Func<IEnumerator>) Delegate.CreateDelegate(enterState.GetType(), behavior, enterMethod);
                }
                else
                {
                    Debug.Log("Enter Method is null.");
                }

                MethodInfo exitMethod = behavior.GetType()
                                                .GetMethod("EnterState", System.Reflection.BindingFlags.Instance
                                                                         | System.Reflection.BindingFlags.Public |
                                                                         System.Reflection.BindingFlags.NonPublic |
                                                                         System.Reflection.BindingFlags.InvokeMethod |
                                                                         System.Reflection.BindingFlags.Static);

                if (exitMethod != null)
                {
                    exitState = (Func<IEnumerator>) Delegate.CreateDelegate(enterState.GetType(),behavior, enterMethod);
                }
                else
                {
                    Debug.Log("Exit Method is null.");
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error creating state: " + behavior.GetType().ToString() + " - " + e.Message);
                Debug.Log(e.StackTrace);
            }
			
			
		}
		
	}
	
	List<State> stateList = new List<State>();

    private Color original;
    private Color highlight;
    public float HighlightTime;
    private bool isHighlighted = false;
	[HideInInspector]
	public State state
	{
		get{return stateList[(int)currentState];}
	}
	
	public Transform _transform;
	public GameObject target;
	public Transform targetTransform;
	
	[HideInInspector]
	public AttackBehavior _attack;
	
	[HideInInspector]
	public FollowingBehavior _following;
	
	[HideInInspector]
	public PatrolBehavior _patrol;

    [HideInInspector]
    public CapturedBehavior _captured;

    [HideInInspector]
    public FlungBehavior _flung;

	EnemyStates _currentState;
    private EnemyStates previousState;

	public EnemyStates currentState
	{	
		get{return _currentState;}
		set{SwitchState(value);}
	}
	
	StateBehavior currentBehavior;
	// Use this for initialization
	void Start ()
	{

	    HighlightTime = 0.5f;
	    original = renderer.material.color;
	    highlight = Color.white;

		_transform = transform;
		_attack = _transform.GetComponent<AttackBehavior>();
		_patrol = _transform.GetComponent<PatrolBehavior>();
		_following = _transform.GetComponent<FollowingBehavior>();
        _captured = _transform.GetComponent<CapturedBehavior>();
        _flung = _transform.GetComponent<FlungBehavior>();
        CheckStates();
		InitializeStates();
		currentState = EnemyStates.PATROL;
	}

    void Highlight()
    {
        Debug.Log("I'm Highlighted!");
        if (!isHighlighted)
        {
            StartCoroutine(TriggerHighlight());
        }
    }

    IEnumerator TriggerHighlight()
    {
        PerformHighlight();
        yield return new WaitForSeconds(HighlightTime);
        EndHighlight();
    }

    private void EndHighlight()
    {
        renderer.material.color = original;
        isHighlighted = false;
    }

    private void PerformHighlight()
    {
        isHighlighted = true;
        renderer.material.color = highlight;
    }

    private void CheckStates()
    {
        if (!_attack)
        {
            Debug.Log("Attack is null");
        }
        if (!_patrol)
        {
            Debug.Log("Patrol is null");

        }
        if (!_following)
        {
            Debug.Log("Following is null");
        }
        if (!_captured)
        {
            Debug.Log("Captured is null");
        }
        if (!_flung)
        {
            Debug.Log("Flung is null");
        }

        if (_attack && _patrol && _following && _captured && _flung)
        {
            Debug.Log("Nothing is null");

        }
    }
	
	void InitializeStates(){
        
            // for each item in enum
            foreach (EnemyStates eState in Enum.GetValues(typeof(EnemyStates)))
            {
                try
                {
                    // reflect on the behaviour
                String bName = "_" + eState.ToString().ToLower();
                
                FieldInfo bField = GetType().GetField(bName);
                if (bField == null)
                {


                    // wire in base null behavior
                }
                else
                {
                    StateBehavior tempBehavior = (StateBehavior)bField.GetValue(this);
                   
                    tempBehavior.SetOwner(this);
                   
                    tempBehavior.DoAwake();
                   
                    
                    State tempState = new State(tempBehavior);
                   

                    stateList.Add(tempState);
                   

                }
                }
                catch(Exception e)
                {
                    Debug.Log("Error setting state " + eState.ToString() + ": " + e.Message);
                    Debug.Log(e.StackTrace);
                }
                

            }
       
		
		
		//Debug.Log("Number of states in list: " + stateList.Count);
	}
	
	// Update is called once per frame
	
	
	public void SwitchState(EnemyStates newState){
		
		if(currentState == newState){
			//Debug.Log("CurrentState == newState");
			return;
		}
		
		// execute exit for current state
		//Debug.Log("Exiting current state: " + currentState.ToString());
		//StartCoroutine(state.exitState());
	    state.DoExit();
		// discover and set up new state
	    previousState = _currentState;
		_currentState = newState;
		
		// execute enter for new state
	    state.DoEnter();
		//StartCoroutine(state.enterState());
		//Debug.Log("Entering new state: " + currentState.ToString());
	}
	
    public void GoToLastState()
    {
        currentState = previousState;
    }

	void Update () {
		state.DoUpdate();
	}
	
	void FixedUpdate(){
		state.DoFixedUpdate();
	}
	
	void LateUpdate(){
		state.DoLateUpdate();
	}
	
	void OnCollisionEnter(Collision collision){
		state.DoOnCollisionEnter(collision);
	}
	
	void OnCollisionExit(Collision collision){
		state.DoOnCollisionExit(collision);
	}
	
	void OnCollisionStay(Collision collision){
		state.DoOnCollisionStay(collision);
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

	
	#endregion

}
