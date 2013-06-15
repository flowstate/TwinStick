using UnityEngine;
using System.Collections;

public class ExitTrigger : MonoBehaviour
{

    public TwinStickShipZed player;
    public Camera moveableCamera;
    public Vector3 newCamLocation, newPlayerLocation;
    public RoomManager previousRoomManager, nextRoomManager;
    public float AnimationTime = 1.0f;
    private Hashtable camTable;

	// Use this for initialization
	void Start () 
    {
	    camTable = new Hashtable();
        camTable.Add("position", newCamLocation);
        camTable.Add("time", AnimationTime);
        camTable.Add("easetype", iTween.EaseType.easeInOutCirc);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // do stuff
            StartCameraMove();
            StartPlayerMove();
            CycleRoomManagers();
        }
    }

    private void CycleRoomManagers()
    {
        previousRoomManager.transform.parent.gameObject.SetActive(false);
        nextRoomManager.transform.parent.gameObject.SetActive(true);
    }

    private void StartPlayerMove()
    {
        player.MoveTween(newPlayerLocation, AnimationTime);
    }

    private void StartCameraMove()
    {
        iTween.MoveTo(moveableCamera.gameObject, camTable);
    }
}
