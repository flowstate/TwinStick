using UnityEngine;
using System.Collections;

public class RisingWall : MonoBehaviour {

	public float desiredHeight = 2.5f;
	public float deltaTime = 1.0f;
    
    public bool WallUp { set; get; }
    Hashtable animateUp, animateDown;
    Vector3 destinationVector;


    // Use this for initialization
	void Start () {
        destinationVector = new Vector3(transform.position.x, desiredHeight, transform.position.z);
        InitHash();
	}

    private void InitHash()
    {
        animateDown = new Hashtable();
        animateUp = new Hashtable();

        //animateUp.Add("name", "raise");
        animateUp.Add("position",destinationVector);
        animateUp.Add("time", deltaTime);
        animateUp.Add("oncomplete", "FinishedRising");

        //animateDown.Add("name", "lower");
        animateDown.Add("position", transform.position);
        animateDown.Add("time", (deltaTime / 2f));
        animateDown.Add("oncomplete", "FinishedLowering");

    }

    public void BeginRaise()
    {
        iTween.MoveTo(gameObject, animateUp);

    }

    public void BeginLower()
    {
        iTween.MoveTo(gameObject, animateDown);

    }

    void FinishedRising()
    {
        WallUp = true;
    }

    void FinishedLowering()
    {
        WallUp = false;
    }

	// Update is called once per frame
	void Update () {
	
	}

	
}
