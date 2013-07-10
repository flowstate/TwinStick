using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerMessage : MonoBehaviour
{

    public bool OnEnter = true;
    public bool OnExit = false;
    public bool UseLayer = true;
    public bool FireOnce = true;
    public bool SendUpwards = false;
    public bool SendColliderWithMessage = false;
    public bool DestroyOnTrigger = false;
    public LayerMask TriggeredLayers;
    public List<string> TriggeredTags;
    public string EnterMessage, ExitMessage;
    public List<GameObject> Targets;

    private bool isActive = true;

	// Use this for initialization
	void Start () {
        
        // check for trigger errors
	    Validate();

	}

    private void Validate()
    {
        if (OnEnter && string.IsNullOrEmpty(EnterMessage))
        {
            throw new System.NullReferenceException("EnterMessage not set for trigger.");
        }

        if (OnExit && string.IsNullOrEmpty(ExitMessage))
        {
            throw new System.NullReferenceException("ExitMessage not set for trigger.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject gObject = other.gameObject;

        if (OnEnter && isActive)
        {
            if (IsTriggered(gObject))
            {
                SendTheMessage(EnterMessage,gObject);
            }

            if (FireOnce)
            {
                isActive = false;
            }

            if (DestroyOnTrigger)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject gObject = other.gameObject;
        if (OnExit && isActive)
        {
            if (IsTriggered(gObject))
            {
                SendTheMessage(ExitMessage, gObject);
            }

            if (FireOnce)
            {
                isActive = false;
            }

            if (DestroyOnTrigger)
            {
                Destroy(gameObject);
            }
        }

    }

    private void SendTheMessage(String theMessage, GameObject gObject)
    {
         
        if (SendUpwards)
        {
            if (SendColliderWithMessage)
            {
                SendMessageUpwards(theMessage, gObject, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                SendMessageUpwards(theMessage, SendMessageOptions.DontRequireReceiver);
            }
        }

        else
        {
            foreach (GameObject target in Targets)
            {
                if (SendColliderWithMessage)
                {
                    target.SendMessage(EnterMessage, gObject, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    target.SendMessage(EnterMessage, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }

    private bool IsTriggered(GameObject other)
    {
        if (UseLayer)
        {
            return Constants.IsInLayerMask(other.gameObject, TriggeredLayers);
        }
        else
        {
            return TriggeredTags.Contains(other.gameObject.tag);
        }
    }



   
}
