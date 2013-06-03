using UnityEngine;
using System.Collections;

public class FeedShots : MonoBehaviour {

    public GameObject shot;
    public KeyCode key;

    public GameObject player;
    Transform tractorArea;

	// Use this for initialization
	void Start () {
        InitPlayer();
	}

    private void InitPlayer()
    {
        tractorArea = player.transform.Find("TractorRoot").Find("TractorArea");
        if (tractorArea == null)
        {
            Debug.Log("Couldn't find it");

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key))
        {
            Instantiate(shot, tractorArea.position, tractorArea.rotation);
        }
	}
}
