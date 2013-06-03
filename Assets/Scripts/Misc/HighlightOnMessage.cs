using UnityEngine;
using System.Collections;

// ReSharper disable CheckNamespace
public class HighlightOnMessage : MonoBehaviour {
// ReSharper restore CheckNamespace

    public float HighlightTime { get; set; }
    private bool isHighlighted = false;
    private Color original;
	// Use this for initialization
	void Start ()
	{
	    HighlightTime = 0.5f;
	    original = renderer.material.color;
	}
	
    void Highlight()
    {
        if (!isHighlighted)
        {
            StartCoroutine(TriggerHighlight());
        }
    }

    IEnumerator TriggerHighlight()
    {
        PerformHighlight();
        yield return new WaitForSeconds(HighlightTime);
        EndHighlight();
    }

    private void EndHighlight()
    {
        renderer.material.color = original;
        isHighlighted = false;
    }

    private void PerformHighlight()
    {
        isHighlighted = true;
        renderer.material.color = Color.blue;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
