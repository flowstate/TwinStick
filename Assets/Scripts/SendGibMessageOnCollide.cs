using UnityEngine;
using System.Collections;

public class SendGibMessageOnCollide : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		col.gameObject.SendMessage("Gib", SendMessageOptions.DontRequireReceiver);
	}
}
