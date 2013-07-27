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
        FLYINGVEE,
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
	    numOccupiedSlots = 0;
        SetUpQueueStuff();
	    InitializeFollowers();

	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Push(null);
            Debug.Log("Num Occupied: " + numOccupiedSlots);
            //CycleFollowers(true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //CycleFollowers(false);
            GameObject temp = Pop();

            if (temp != null)
            {
                Destroy(temp);
            }
        }

        if (Input.GetButtonDown("Formation"))
        {
            CycleQueueType();
        }

    }

    private void InitializeFollowers()
    {
        int i = 0;
        foreach (FollowerSlot slot in FollowerList)
        {
            slot.CurrentPosition = i++;
            if (slot.Filler != null)
            {
                numOccupiedSlots++;
            }
        }

        SetFollowerPositions();

    }

    private void SetFollowerPositions()
    {
        Debug.Log("Setting Positions");
        if (numOccupiedSlots > 0)
        {
            for (int i = 0; i < FollowerList.Count; i++)
            {
                FollowerList[i].VectorOffset = SetPosition(FollowerList[i].CurrentPosition);
            }    
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
                return SetBarrierPosition(position+1);

            case QueueTypes.CIRCLE:
                return SetCirclePosition(position + 1);
                break;

            case QueueTypes.FLYINGVEE:
                return SetFlyingVeePosition(position);
                break;

            default:
                Debug.Log("UNDEFINED QUEUE TYPE: " + currentQueueType.ToString());
                throw new SystemException();
                break;
        }

        return new Vector3(xPos, 0, zPos);
    }

    private Vector3 SetCirclePosition(int posPlusOne)
    {
        float angle = ((360/numOccupiedSlots) * posPlusOne) + 180;
        Vector3 position = Quaternion.AngleAxis(angle, Vector3.up) * (Vector3.forward * 2.5f);
        Debug.Log("Set circle position, position: " + position.ToString("0.00"));

        return position;

    }

    private Vector3 SetFlyingVeePosition(int pos)
    {
        bool isOdd = (pos+1) % 2 == 1;
        float xPos = 0, zPos = 0;

        xPos = isOdd ? (pos + 1)*-1 : (pos);
        //zPos = isOdd ? -(pos + 1) : -(pos);
        zPos = isOdd ? -(pos) : -(pos - 1);

        //if (isOdd)
        //{
        //    xPos = (pos + 2) *-1;
        //    zPos = -(pos + 1);
        //}
        //else
        //{
        //    xPos = (pos + 1);
        //    zPos = -(pos);
        //}
        return new Vector3(xPos,0,zPos);
    }

    private Vector3 SetBarrierPosition(int posPlusOne)
    {
        float fullDegree = (180/numOccupiedSlots);
        float tempDegree = (fullDegree/2) + ((posPlusOne - 1) * fullDegree);
        Vector3 position = Quaternion.AngleAxis(tempDegree, Vector3.up)*(Vector3.left*2.5f);

        return position;

        float xPos = 0, zPos = 0;
        bool isOdd = (numOccupiedSlots%2 == 1);

        Debug.Log("Num Slots: " + numOccupiedSlots + "\nisOdd: " + isOdd);

        switch (posPlusOne)
        {
            case 1:
                zPos = Mathf.Ceil(numOccupiedSlots / 2) * 2;
                xPos = isOdd ? 0 : -1;
                break;
            case 2:
                zPos = Mathf.Floor(numOccupiedSlots / 2) * 2;
                xPos = isOdd ? -1 : 1;

                break;
            case 3:
                xPos = isOdd ? 2 : 3;
                zPos = numOccupiedSlots > 4 ? 4 : 3;

                break;
            case 4:
                xPos = isOdd ? -4 : -3;
                zPos = numOccupiedSlots > 5 ? 4 : 2;
                break;
            case 5:
                xPos = isOdd ? 3 : 4;
                zPos = 2;
                break;
            case 6:
                xPos = 3;
                zPos = 2;
                break;
            default:
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

        FollowerSlot first = GetFirstFollower();

        if (first.Filler != null)
        {
            Debug.Log("OCCUPADO!");
            first.DestroyFiller();
            numOccupiedSlots--;
        }

        if (null == filler)
        {
            //filler = GameObject.CreatePrimitive(PrimitiveType.Cube);
            filler = Instantiate(FSPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            //filler.layer = LayerMask.NameToLayer("PlayerBullets");
        }
        
        first.Filler = filler;
        numOccupiedSlots++;
        SetFollowerPositions();
    }

    public GameObject Pop()
    {
        GameObject filler = GetFirstFollower().Filler;
        if (filler != null)
        {
            GetFirstFollower().PopFiller();
            numOccupiedSlots--;
            CycleFollowers(false);
            
            
        }
        SetFollowerPositions();
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

        SetFollowerPositions();
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
