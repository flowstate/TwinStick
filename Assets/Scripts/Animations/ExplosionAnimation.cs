using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour
{

    public float FirstPhaseLength = 2f;
    public float SecondPhaseLength = 1.5f;
    public float ThirdPhaseLength = 1f;
    public int LoopsPerPhase = 8;
    public bool SendMessage = false;
    public string MethodName = "Boom";
    public Color ColorFrom = Color.green;
    public Color ColorTo = Color.blue;
    private Color originalColor;
    private Hashtable phaseTable;
    private bool isFlashing = false;
    private int currentHalfLoop = 0;


	// Use this for initialization
	void Start ()
	{
	    originalColor = renderer.material.color;
	    
	    InitTables();
	}

    private void InitTables()
    {
        phaseTable = new Hashtable();
        

        phaseTable.Add("name", "first");
        
        phaseTable.Add("easetype", iTween.EaseType.linear);
        phaseTable.Add("color", ColorTo);
        phaseTable.Add("time", (FirstPhaseLength / LoopsPerPhase));
        phaseTable.Add("looptype", iTween.LoopType.pingPong);
        phaseTable.Add("oncomplete", "FirstIterate");

    }

    void StartFirstPhase()
    {
        renderer.material.color = ColorFrom;
        iTween.ColorTo(gameObject, phaseTable);
        isFlashing = true;
    }

    void FirstIterate()
    {
        if (++currentHalfLoop >= LoopsPerPhase)
        {
            iTween.StopByName(gameObject, "first");
            currentHalfLoop = 0;
            renderer.material.color = ColorFrom;
            StartSecondPhase();    
        }
    }

    void StartSecondPhase()
    {
        phaseTable["oncomplete"] = "SecondIterate";
        phaseTable["time"] = SecondPhaseLength/LoopsPerPhase;
        iTween.ColorTo(gameObject, phaseTable);
    }

    void SecondIterate()
    {
        if (++currentHalfLoop >= LoopsPerPhase)
        {
            iTween.StopByName(gameObject, "first");
            currentHalfLoop = 0;
            renderer.material.color = ColorFrom;
            StartThirdPhase();
        }
    }

    private void StartThirdPhase()
    {
        phaseTable["oncomplete"] = "ThirdIterate";
        phaseTable["time"] = ThirdPhaseLength / LoopsPerPhase;
        iTween.ColorTo(gameObject, phaseTable);
    }

    void ThirdIterate()
    {
        if (++currentHalfLoop >= LoopsPerPhase)
        {
            iTween.StopByName(gameObject, "first");
            currentHalfLoop = 0;
            renderer.material.color = ColorFrom;
            Boom();
        }
    }

   

    private void Boom()
    {
        Debug.Log("BOOOOOOOOOOOM");
        isFlashing = false;
        renderer.material.color = originalColor;

        if (SendMessage)
        {
            gameObject.SendMessage(MethodName, SendMessageOptions.DontRequireReceiver);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (!isFlashing)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
               StartFirstPhase();
            }    
        }
	    
	}
}
