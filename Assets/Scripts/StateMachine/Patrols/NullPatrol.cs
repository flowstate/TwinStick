using UnityEngine;
using System.Collections;

public class NullPatrol : PatrolBehavior {
	
	

    public override void DoEnter()
    {
        
    }

    public override void DoExit()
    {
        
    }
	
	
	
	public override void DoUpdate ()
	{
		if(owner.target != null){
			owner.currentState = EnemyStates.FOLLOWING;
		}else{
			
		}
	}
	
}
