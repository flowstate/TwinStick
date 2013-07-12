using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour
{


    public int NumHits;
    public LayerMask hitLayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (Constants.IsInLayerMask(col.gameObject, hitLayer))
        {
            TakeHit();
        }
    }

    private void TakeHit()
    {
        NumHits--;

        if (NumHits < 1)
        {
            Destroy(gameObject);
        }
    }
}
