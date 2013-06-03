using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeakPointBoss : MonoBehaviour {
	
	public GameObject nextPhase;
	
	
	int numWeakPoints = 3;
	int currentWeakPoints = 3;
	List<GameObject> wpList;
	
	// Use this for initialization
	void Start () {
		wpList = new List<GameObject>();
		
		PopulateWeakPoints();
	}
	
	void PopulateWeakPoints(){
		foreach(Transform child in transform){
			if(child.gameObject.name.Equals("WeakPoint")){
				
				wpList.Add(child.gameObject);
			}
		}
		
		Debug.Log("I have " + wpList.Count + " weak points. Please don't shoot me!");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.K)){
			// kill a weakpoint
			wpList[currentWeakPoints - 1].SetActive(false);		
			WeakPointKilled();
		}
	}

	void DetectWeakPoint(GameObject wPoint)
	{
		Debug.Log("Detecting weak point");
		if (wpList.Contains(wPoint))
		{
			Debug.Log("Found the weak point");
			GameObject detected = wpList.Find(item => item.Equals(wPoint));


		}
	}

	public void WeakPointKilled(){
		currentWeakPoints--;
		Debug.Log("Killed a weakpoint. I have " + currentWeakPoints + " left.");
		if(currentWeakPoints == 0){
			AllWeakPointsDestroyed();
		}
		
	}
	
	void AllWeakPointsDestroyed(){
		Debug.Log("Damn, I'm dead");
		
		StartNextPhase();
	}
	
	void ResetWeakPoints(){
		foreach(GameObject child in wpList){
			child.SetActive(true);
		}
		currentWeakPoints = wpList.Count;
	}
	
	void StartNextPhase(){
		if(nextPhase != null){
			nextPhase.SetActive(true);
		}
		
		Destroy(this.gameObject);
	}
}
