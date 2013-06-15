using UnityEngine;
using System.Collections;

public class ArcingShot : MonoBehaviour
{

    public Vector3 Apex = new Vector3(0,5,0);
    public Vector3 Landing = new Vector3(0,0,5);
    public float Time;
    public GameObject SpawnOnStrike;
    public iTween.EaseType Easing;
    private Hashtable shotTable;
    private Vector3[] _path;
    public GameObject Owner, ShotMarker;
    private bool _shotsFired = false;
	// Use this for initialization
	void Start () 
    {
	    shotTable = new Hashtable();
    }

    public void SetShot(Vector3[] shotPath, float time, GameObject owner, GameObject marker)
    {
        Owner = owner;
        ShotMarker = marker;
        _path = shotPath;
        Time = time;
        InitializeTable();
    }

    private void InitializeTable()
    {
        shotTable.Add("path", _path);
        shotTable.Add("time", Time);   
        shotTable.Add("easetype", Easing);
        if (Owner != null)
        {
            shotTable.Add("onstart", "ShotsFired");
            shotTable.Add("onstarttarget", Owner);
        }
        shotTable.Add("oncomplete", "Thuderstruck");
        shotTable.Add("oncompletetarget", gameObject);
    }
	
    public void Thunderstruck()
    {
        if (ShotMarker != null)
        {
            Destroy(ShotMarker);
        }
        if (SpawnOnStrike == null)
        {
            Destroy(gameObject);
        }
    }

    public void InitializePath()
    {
        _path[0] = transform.position;
        _path[1] = Apex;
        _path[2] = Landing;
    }

    void Update()
    {
        
    }

	public void Fire()
	{
	    _shotsFired = true;
	    iTween.MoveTo(gameObject, shotTable);
	}
}
