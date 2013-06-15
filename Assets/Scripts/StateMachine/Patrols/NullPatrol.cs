using UnityEngine;
using System.Collections;

public class NullPatrol : PatrolBehavior {
	
	public IEnumerator EnterState(){
		Debug.Log("I'm patrolling.");
		yield break;
	}

    public override void DoEnter()
    {
        Debug.Log("Entering Patrollllllll");
    }

    public override void DoExit()
    {
        Debug.Log("Exiting Patrolllllll");
    }
	
	// Update is called once per frame
	void Update () {
		
	
	}
	
	public override void DoUpdate ()
	{
		if(owner.target != null){
			owner.currentState = EnemyStates.FOLLOWING;
		}else{
			Debug.Log("I DONT KNOW WHO TO SHOOT!");
		}
	}
	
	public IEnumerator ExitState(){
		Debug.Log("I'm no longer patrolling.");
		yield break;
	}
}
