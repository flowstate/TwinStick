using UnityEngine;
using System.Collections;

public class Basic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Hashtable ht = new Hashtable();
		ht.Add("x",3);
		ht.Add("time", 4);
		ht.Add("looptype", iTween.LoopType.pingPong);
		iTween.Init(gameObject);
		iTween.MoveTo(gameObject,ht);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
