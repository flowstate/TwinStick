using UnityEngine;
using System.Collections;

public class IncrementKillsOnDestroy : MonoBehaviour {
	
	void OnDestroy(){
		KillManager.killCount++;	
	}
}
