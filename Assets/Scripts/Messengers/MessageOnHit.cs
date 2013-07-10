using UnityEngine;
using System.Collections;

public class MessageOnHit : MonoBehaviour
{

    public LayerMask HurtLayers;
    public string HurtMessage;
    public bool SendUpwards = true;
    public bool OnEnter = true;
    public bool OnExit = false;
    public int StartingHits = 3;
    private int currentHits;
    public bool SendColliderWithMessage = false;
    public bool DebugHit = false;
    public KeyCode KeyForHit;
    public bool SendOnZeroOnly = false;

	// Use this for initialization
	void Start ()
	{
	    currentHits = StartingHits;
	}
	
	// Update is called once per frame
	void Update () {

        if (DebugHit)
        {
            if (Input.GetKeyDown(KeyForHit))
            {
                SendTheMessage(null);
            }
        }

	}

    void OnCollisionEnter(Collision col)
    {
        if (OnEnter)
        {
            if (Constants.IsInLayerMask(col.gameObject, HurtLayers))
            {
                SendTheMessage(col.gameObject);
            }
        }
    }

    

    void OnCollisionExit(Collision col)
    {
        if (OnExit)
        {
            if (Constants.IsInLayerMask(col.gameObject, HurtLayers))
            {
                SendTheMessage(col.gameObject);    
            }
            
        }
    }

    private void SendTheMessage(GameObject gameObject)
    {
        if (SendOnZeroOnly && --currentHits > 0)
        {
            Debug.Log("Not dead yet. Hits left: " + currentHits);
            return;
        }

        Debug.Log("Sending message.");

        if (SendUpwards)
        {
            if (SendColliderWithMessage)
            {
                SendMessageUpwards(HurtMessage, gameObject, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                SendMessageUpwards(HurtMessage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
