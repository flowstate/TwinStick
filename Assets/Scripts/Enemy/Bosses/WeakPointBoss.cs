using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeakPointBoss : MonoBehaviour {
	
	public GameObject nextPhase;
	
	
	int numWeakPoints = 3;
	int aliveWeakPoints = 3;
	List<GameObject> wpList;
    public float firingDelay = 2.0f;
    public GameObject projectile;
    public GameObject enemySpawningProjectile;

	
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
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			// kill a weakpoint
			StartNextPhase();
		}
	}

	public void WeakPointKilled(){
		aliveWeakPoints--;
		Debug.Log("Killed a weakpoint. I have " + aliveWeakPoints + " left.");
		if(aliveWeakPoints == 0){
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
		aliveWeakPoints = wpList.Count;
	}
	
    public void PlayerInRange()
    {
        // start the firing
    }

	void StartNextPhase(){
		if(nextPhase != null)
		{
		    nextPhase.transform.parent = null;
			nextPhase.SetActive(true);
		}
		
		Destroy(this.gameObject);
	}

    
}
