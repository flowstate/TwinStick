using UnityEngine;
using System.Collections;

public class BaseDoor : MonoBehaviour
{
    public bool IsDoorUp = false;

    public RoomManager rManager;

	// Use this for initialization
	void Start ()
	{
	    InitiateAnimations();
	}

    protected virtual void InitiateAnimations()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}



    public virtual void CloseTheDoor()
    {
        IsDoorUp = true;
    }

    public virtual void OpenTheDoor()
    {
        Debug.Log("Opening the door!");
        IsDoorUp = false;
    }
}
