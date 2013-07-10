using UnityEngine;
using System.Collections;

public class SlidingDoor : BaseDoor
{

    public Transform OpenPosition, ClosedPosition;
    public float TweenTime = 1.5f;
    private Hashtable openTable, closeTable;
    public GameObject PhysicalDoor;

    // Use this for initialization
	void Start ()
	{
	    InitTables();
	}

    private void InitTables()
    {
        openTable = new Hashtable();
        closeTable = new Hashtable();
        
        openTable.Add("time", TweenTime);
        closeTable.Add("time", TweenTime);

        openTable.Add("name", "open");
        closeTable.Add("name", "close");

        openTable.Add("position", OpenPosition.position);
        closeTable.Add("position", ClosedPosition.position);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void CloseTheDoor()
    {
        iTween.MoveTo(PhysicalDoor, closeTable);
        base.CloseTheDoor();
    }

    public override void OpenTheDoor()
    {
        iTween.MoveTo(PhysicalDoor, openTable);
        base.OpenTheDoor();
    }
}
