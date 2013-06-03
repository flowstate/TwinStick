using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour
{

    public bool DisplayAngle = true;
    public bool DisplayRawInput = true;

    public UILabel AngleDisplay;
    public UILabel InputDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (DisplayAngle)
        {
            OutputAngle();
        }

        if (DisplayRawInput)
        {
            OutputRawInput();
        }
	}

    private void OutputRawInput()
    {
        if (InputDisplay != null)
        {
            InputDisplay.text = Input.GetAxis("Vertical").ToString("0.00") + "," + Input.GetAxis("Horizontal").ToString("0.00");
            

        }
    }

    private void OutputAngle()
    {
        if (AngleDisplay != null)
        {
            
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            AngleDisplay.text = Vector2.Angle(Vector2.up, input).ToString("000.0");
        }
    }
}
