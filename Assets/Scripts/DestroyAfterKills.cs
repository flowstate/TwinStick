using UnityEngine;
using System.Collections;

public class DestroyAfterKills : MonoBehaviour {
	
	public int killsGoal = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(KillManager.killCount >= killsGoal){
			Destroy(gameObject);
		}
	}
}
