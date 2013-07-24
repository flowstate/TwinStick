using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueueManager : MonoBehaviour
{
    public enum QueueTypes
    {
        LINE = 0,
        BARRIER_FRONT,
        PHALANX,
        CIRCLE
    }



    public int QueueSize = 3;
    public List<FollowerSlot> FollowerList;
    public GameObject FSPrefab;
    public QueueTypes currentQueueType = QueueTypes.LINE;
    private int sumQueueTypes;
    private QueueTypes lastQueueType;
    private int numOccupiedSlots;

    public float LineZedOffset = -2f;

	// Use this for initialization
	void Start ()
	{
	    
        SetUpQueueStuff();

        //if (FollowerList == null)
        //{
        //    FollowerList = new List<FollowerSlot>();
        //    PopulateFollowers();
        //}

	    InitializeFollowers();

	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            CycleFollowers(true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            CycleFollowers(false);
        }

    }

    private void InitializeFollowers()
    {
        int i = 0;
        foreach (FollowerSlot slot in FollowerList)
        {
            slot.CurrentPosition = i++;
        }

        SetFollowerPositions();

    }

    private void SetFollowerPositions()
    {
        Debug.Log("Setting Positions");
        for (int i = 0; i < FollowerList.Count; i++)
        {
            FollowerList[i].VectorOffset = SetPosition(FollowerList[i].CurrentPosition);
        }
    }

    private Vector3 SetPosition(int position)
    {
        float xPos, zPos;
        xPos = zPos = 0;
        switch (currentQueueType)
        {
            case QueueTypes.LINE:
                zPos = (position + 1) * LineZedOffset;
                break;

            case QueueTypes.BARRIER_FRONT:

                break;
            case QueueTypes.CIRCLE:

                break;

            case QueueTypes.PHALANX:

                break;

            default:
                Debug.Log("UNDEFINED QUEUE TYPE: " + currentQueueType.ToString());
                throw new SystemException();
                break;
        }

        return new Vector3(xPos, 0, zPos);
    }

    public void FillerGone(int followerPosition)
    {
        Debug.Log("The Filler's Gone, away.");
    }

    public void Push(GameObject filler)
    {
        CycleFollowers(true);
        GetFirstFollower().Filler = filler;

    }

    public GameObject Pop()
    {
        GameObject filler = GetFirstFollower().Filler;
        GetFirstFollower().PopFiller();
        return filler;
    }


    private FollowerSlot GetFirstFollower()
    {
        foreach (FollowerSlot slot in FollowerList)
        {
            if (slot.CurrentPosition == 0)
            {
                return slot;
            }
        }

        return null;
    }

    

    private void SetUpQueueStuff()
    {
        sumQueueTypes = Enum.GetNames(typeof(QueueTypes)).Length - 1;
        
        lastQueueType = (QueueTypes)Enum.GetNames(typeof(QueueTypes)).Length - 1;
        numOccupiedSlots = FollowerList.Count;
    }

    public void CycleQueueType()
    {
        

        Debug.Log("Current queue type: " + currentQueueType.ToString());
        if (currentQueueType >= lastQueueType)
        {
            currentQueueType = (QueueTypes) 0;
        }
        else
        {
            currentQueueType++;
        }
        
        Debug.Log("New queue type: " + currentQueueType.ToString());
    }

	



    public void CycleFollowers(bool toBack)
    {
        for (int index = 0; index < FollowerList.Count; index++)
        {
            // if we're moving back
            if (toBack)
            {
                //if this is the last in line, move to front
                if (FollowerList[index].CurrentPosition == FollowerList.Count - 1)
                {
                    FollowerList[index].CurrentPosition = 0;
                }
                else
                {
                    FollowerList[index].CurrentPosition++;
                }
                
            }

            // if we're moving forward
            if (!toBack)
            {
                // if this is the first, move to the back
                if (FollowerList[index].CurrentPosition == 0)
                {
                    FollowerList[index].CurrentPosition = FollowerList.Count - 1;
                }
                else
                {
                    FollowerList[index].CurrentPosition--;
                }
            }
        }

        SetFollowerPositions();
    }
}
