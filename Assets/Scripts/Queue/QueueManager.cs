using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class QueueManager : MonoBehaviour
{
    public enum QueueTypes
    {
        LINE,
        BARRIER_FRONT,
        CIRCLE
    }



    public int QueueSize = 3;
    public List<FollowerSlot> followers;
    public GameObject FSPrefab;
    public QueueTypes currentQueueType = QueueTypes.LINE;
    
	// Use this for initialization
	void Start () {
	    if (followers == null)
	    {
            followers = new List<FollowerSlot>();
	        PopulateFollowers();
	    }

	}

    

    private void PopulateFollowers()
    {
        for (int i = 0; i < QueueSize; i++)
        {
            
            //followers.Add();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CycleFollowers(bool toBack)
    {
        foreach (FollowerSlot slot in followers)
        {
            
        }
    }
}
