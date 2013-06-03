using UnityEngine;
using System.Collections;

public class AnimateColor : MonoBehaviour {
	
	public Color startingColor = Color.red;
	public Color endingColor = Color.yellow;
	public float time = 1.0f;
	Hashtable colorTable;
	
	// Use this for initialization
	void Start () {
		colorTable = new Hashtable();
		colorTable.Add("color", endingColor);
		colorTable.Add("time", time);
		colorTable.Add("loopType", iTween.LoopType.pingPong);
		
		iTween.ColorTo(gameObject, colorTable);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
