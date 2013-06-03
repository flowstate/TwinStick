using UnityEngine;
using System.Collections;

public class SecondPhase : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K)){
			TakeHit();
			
		}
	}
	
	void TakeHit(){
		Destroy(gameObject);
	}
}
