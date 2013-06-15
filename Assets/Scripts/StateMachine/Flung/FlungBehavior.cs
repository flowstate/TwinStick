using UnityEngine;
using System.Collections;

public class FlungBehavior : StateBehavior 
{
    public override void DoUpdate() { }

    public override void DoAwake() { }

    //public override void DoLateUpdate() { }
    //public override void DoFixedUpdate() { }

    //public override void DoOnMouseEnter() { }
    //public override void DoOnMouseUp() { }
    //public override void DoOnMouseDown() { }
    //public override void DoOnMouseOver() { }
    //public override void DoOnMouseExit() { }
    //public override void DoOnMouseDrag() { }

    public override void DoOnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    
    //public override void DoOnCollisionExit(Collision collision) { }
    //public override void DoOnCollisionStay(Collision collision) { }

    //public override void DoOnTriggerEnter(Collider col) { }
    //public override void DoOnTriggerExit(Collider col) { }
    //public override void DoOnTriggerStay(Collider col) { }

    //public override void DoEnter() { }

    

    public IEnumerator EnterState()
    {
        Debug.Log("Entered flung behavior. Let's get flung.");
        yield break;
    }

    public IEnumerator ExitState()
    {
        Debug.Log("Exited flung behavior. Let's get flung.");
        yield break;
    }

}
