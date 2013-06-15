using UnityEngine;
using System.Collections;

public class HighlightRay : MonoBehaviour
{

    private Transform _transform;
    public LayerMask collisionMask;
    private bool isAimSlowed = false;
    private TractorZed _tractor;
    // Use this for initialization
	void Start ()
	{
	    _transform = transform;
	    _tractor = _transform.parent.GetComponent<TractorZed>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!_tractor.IsAimSlowed)
	    {
            CastTheRay();
	    }
	    
	}

    private void CastTheRay()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,
                             collisionMask)) return;

        GameObject victim = hit.transform.gameObject;
        Debug.Log("I HIT AN ENEMY");
        victim.SendMessage("Highlight", SendMessageOptions.DontRequireReceiver);
        

        StartCoroutine(_tractor.FreezeAim());
    }

    
}
