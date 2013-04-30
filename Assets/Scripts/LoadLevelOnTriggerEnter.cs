using UnityEngine;
using System.Collections;

public class LoadLevelOnTriggerEnter : MonoBehaviour {
	
	public string playerTag = "Player";
	public string levelName;
	public bool isActive = true;
	
	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag == playerTag){
			Application.LoadLevel(levelName);
		}
	}
}
