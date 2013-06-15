using UnityEngine;
using System.Collections;

public class CapturedBehavior : StateBehavior {

    public float fireTimer = 0.5f;
    public GameObject projectile;
    public float fireSpeed = 5.0f;

    public override void DoUpdate() 
    {
    
    }

    public override void DoAwake() { }

    //public override void DoLateUpdate() { }
    //public override void DoFixedUpdate() { }

    //public override void DoOnMouseEnter() { }
    //public override void DoOnMouseUp() { }
    //public override void DoOnMouseDown() { }
    //public override void DoOnMouseOver() { }
    //public override void DoOnMouseExit() { }
    //public override void DoOnMouseDrag() { }

    //public override void DoOnCollisionEnter(Collision collision) { }
    //public override void DoOnCollisionExit(Collision collision) { }
    //public override void DoOnCollisionStay(Collision collision) { }

    //public override void DoOnTriggerEnter(Collider col) { }
    //public override void DoOnTriggerExit(Collider col) { }
    //public override void DoOnTriggerStay(Collider col) { }

    //public override void DoEnter() { }

    IEnumerator FireForward()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireTimer);
            FireShot();
        }
    }

    private void FireShot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        bullet.rigidbody.AddForce(transform.forward.normalized * fireSpeed);
    }

    public IEnumerator EnterState()
    {
        Debug.Log("Entered Captured State. Holler");
        

        yield break;
    }

    public IEnumerator ExitState()
    {
        
        yield break;
    }
}
