using UnityEngine;
using System.Collections;

public class WeakPoint : MonoBehaviour {

	WeakPointBoss parentBoss;

	// Use this for initialization
	void Start () {
		Initialize();
	}
	
	void Initialize(){
		parentBoss = transform.parent.GetComponent<WeakPointBoss>();
		if (parentBoss == null)
		{
			Debug.Log("Parent is null");
		}
		else
		{
			Debug.Log("MAMA!");
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			HitTaken();
		}
	}

	void HitTaken()
	{
		parentBoss.WeakPointKilled();
		gameObject.SetActive(false);
	}

	void OnCollisionEnter()
	{
		// check collision layer mask
		HitTaken();
	   
	}
}
