using UnityEngine;
using System.Collections;

public class TargetProjectile : MonoBehaviour
{

    public LayerMask DentLayers;
    public int numHits = 2;
    public LayerMask BreakLayers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (Constants.IsInLayerMask(col.gameObject, BreakLayers))
        {
            Destroy(gameObject);
        }
        if (Constants.IsInLayerMask(col.gameObject, DentLayers))
        {
            TakeHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (Constants.IsInLayerMask(other.gameObject, BreakLayers))
        {
            Destroy(gameObject);
        }
    }

    private void TakeHit()
    {
        numHits--;
        if (numHits <= 0)
        {
            Destroy(gameObject);
        }
    }
}
