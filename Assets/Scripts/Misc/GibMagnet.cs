using UnityEngine;
using System.Collections;

public class GibMagnet : MonoBehaviour
{

    public LayerMask GibLayers;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (Constants.IsInLayerMask(other.gameObject, GibLayers))
        {
            SeekTarget seeker = other.gameObject.GetComponent<SeekTarget>();
            seeker.SetTarget(gameObject);
        }
        else
        {
            Debug.Log("Not in layermask, apparently");
        }
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    if (Constants.IsInLayerMask(col.gameObject, GibLayers))
    //    {
    //        SeekTarget seeker = col.gameObject.GetComponent<SeekTarget>();
    //        seeker.SetTarget(gameObject);
    //    }
    //}
}
