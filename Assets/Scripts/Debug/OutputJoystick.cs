using UnityEngine;
using System.Collections;

public class OutputJoystick : MonoBehaviour
{
    private UILabel label;

	// Use this for initialization
	void Start ()
	{
	    label = gameObject.GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    label.text = "Joystick output -  X: " + Input.GetAxis("Horizontal").ToString("00.0") + " Y: " +
	                 Input.GetAxis("Vertical").ToString("00.0");
	}
}
