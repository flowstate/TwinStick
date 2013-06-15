using UnityEngine;
using System.Collections;

public class TweenOnCommand : MonoBehaviour
{


    private Hashtable tweenTable;
	// Use this for initialization
	void Start () {
	    
        tweenTable = new Hashtable();
        tweenTable.Add("x", 0f);
        tweenTable.Add("z", -42f);
        tweenTable.Add("time", 3f);
        tweenTable.Add("easetype", iTween.EaseType.linear);
        tweenTable.Add("oncomplete", "ResumePlayerControl");
        tweenTable.Add("oncompletetarget", gameObject);
        tweenTable.Add("onstart", "DisablePlayerControl");
        tweenTable.Add("onstarttarget", gameObject);
	}
	
    public void Tween()
    {
        iTween.MoveTo(gameObject, tweenTable);
    }

	// Update is called once per frame
	void Update () 
    {
	}
}
