using UnityEngine;
using System.Collections;

public class CombinablePiece : MonoBehaviour {
	
	public string validTag = "TestPiece";
	public GameObject wholePrefab;
	public bool hitFirst = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision){
		
		// if we collided with a brother object
		if(hitFirst){
			if(collision.gameObject.tag.Contains(validTag)){
				Debug.Log("Hello, brother!");
				collision.gameObject.GetComponent<CombinablePiece>().hitFirst = false;
				Destroy(collision.gameObject);
				Instantiate(wholePrefab, collision.contacts[0].point,Quaternion.identity);
				Destroy(gameObject);
			}
		}
		
		else{
			//Debug.Log("Get thee behind me, stranger!");
		}
	}
}
