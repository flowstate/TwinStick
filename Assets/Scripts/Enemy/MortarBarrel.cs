using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MortarBarrel : MonoBehaviour
{

    public GameObject Projectile;
    public float FireTime = 1.5f;
    public bool RepeatFire = true;
    private List<GameObject> _shotMarkers;
    public GameObject hackMarker;
    public float ApexHeight = 50f;
    public bool isFiring = false;
    public GameObject BulletStart;
	// Use this for initialization
	void Start () {
	    _shotMarkers = new List<GameObject>();
	}

    public void AddFiringOrder(GameObject newMarker)
    {
        Debug.Log("Staring firing order");
        if (null == newMarker)
        {
            Debug.Log("NewMarker is null");
            newMarker = hackMarker;
        }
        _shotMarkers.Add(newMarker);
        Debug.Log("Added a firing order, count is " + _shotMarkers.Count );
    }

    public void StartFire()
    {
        if (!isFiring)
        {
            StartCoroutine(TimedFire());
        }
    }

    public IEnumerator TimedFire()
    {
        isFiring = true;
        while (_shotMarkers.Count > 0)
        {
            Debug.Log("Shot count is " + _shotMarkers.Count);
            yield return new WaitForSeconds(FireTime);
            Fire();
        }
        isFiring = false;
    }

    private void Fire()
    {
        GameObject tempMarker = _shotMarkers[0];
        _shotMarkers.RemoveAt(0);
        Debug.Log("In Firing, shot count " + _shotMarkers.Count);
        
        // generate path
        Vector3[] path = GeneratePath(tempMarker.transform.position);
        
        // instantiate arcing shot
        GameObject tempShot = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
        ArcingShot arc = tempShot.GetComponent<ArcingShot>();

        arc.SetShot(path, FireTime, gameObject, tempMarker);

        // fire
        arc.Fire();
    }

    private Vector3[] GeneratePath(Vector3 markerPos)
    {
        
        List<Vector3> pathList = new List<Vector3>();
        pathList.Add(BulletStart.transform.position);
        pathList.Add(new Vector3((markerPos.x - transform.position.x) / 2, ApexHeight, (markerPos.z - transform.position.z)));
        pathList.Add(markerPos);

        return pathList.ToArray();
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddFiringOrder(null);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartFire();
        }
	}
}
