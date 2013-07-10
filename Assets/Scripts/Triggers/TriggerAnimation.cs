using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour {

    public GameObject wallToTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggering Wall.");
            wallToTrigger.SetActive(true);
        }
    }
}
