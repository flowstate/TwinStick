using UnityEngine;
using System.Collections;

public class ShootingTarget : MonoBehaviour
{

    public LayerMask ShooterMask;
    public int TotalHits;
    public Transform UpPosition, DownPosition;
    private int currentHits = 0;
    private string LastLayerHit;
    public float AnimationTime;
    public bool isActive = false;
    private Hashtable upTable;
    public GameObject TargetTrigger;
    public GameObject TargetBody;
    public TargetManager Manager;

	// Use this for initialization
	void Start () {
	    upTable = new Hashtable();
        upTable.Add("position", UpPosition.position);
        upTable.Add("oncomplete", "FinishedReset");
        upTable.Add("oncompletetarget", gameObject);
	}

    private void TakeHit(GameObject hitter)
    {
        currentHits++;
        LastLayerHit = LayerMask.LayerToName(hitter.layer);
        
        if (currentHits >= TotalHits)
        {
            TellManager(hitter);
            Disable();
        }
    }

    private void TakeHit(Collider col)
    {
        TakeHit(col.gameObject);
    }

    private void TellManager(GameObject hitter)
    {
        Manager.TargetDown(hitter);
    }



    public void Disable()
    {
     
        TargetTrigger.SetActive(false);
        isActive = false;
        StopCoroutine("TimedEnable");
        iTween.MoveTo(TargetBody, DownPosition.position, AnimationTime);
        // send layer reference as well as "I'm down" to scoremanager
    }

    public void EnableTarget()
    {
        iTween.MoveTo(TargetBody, upTable);
    }

    public void EnableTimed(float time)
    {
        Debug.Log("Started EnableTimed");
        StartCoroutine("TimedEnable", time);
    }

    private IEnumerator TimedEnable(float time)
    {
        EnableTarget();
        yield return new WaitForSeconds(time);
        Disable();
    }

    private void FinishedReset()
    {
        isActive = true;
        TargetTrigger.SetActive(true);
        currentHits = 0;
        LastLayerHit = null;
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A) && isActive)
	    {
	       TakeHit(gameObject);
	    }
	}
}
