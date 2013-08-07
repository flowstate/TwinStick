using UnityEngine;
using System.Collections;

public class DrawShotLine : MonoBehaviour
{

    public LineRenderer LineRenderer;
    public Transform Origin, ShortDestination, LongDestination;
    private Vector3 startPos, shortEndPos, fullEndPos, currentEndPos;
    public float AnimateSpeed, HoldTime, LineWidth;
    public bool isLineDrawing = false;
	// Use this for initialization
	void Start () {

        startPos = Origin.position;
        shortEndPos = ShortDestination.position;
	    currentEndPos = shortEndPos;
        fullEndPos = LongDestination.position;
        LineRenderer.SetPosition(0, startPos);
        LineRenderer.SetPosition(1, startPos);
        LineRenderer.SetWidth(LineWidth, LineWidth);
	    LineRenderer.useWorldSpace = false;

	}
	
    public IEnumerator StartLineDraw()
    {
        
        isLineDrawing = true;
        float elapsedTime = 0, fraction;

        while (elapsedTime < AnimateSpeed)
        {
            elapsedTime += Time.deltaTime;
            fraction = elapsedTime/AnimateSpeed;
            currentEndPos = Vector3.Lerp(currentEndPos, fullEndPos, fraction);
            LineRenderer.SetPosition(1, currentEndPos);
            yield return null;
        }

        yield return new WaitForSeconds(HoldTime);

        elapsedTime = 0;

        while (elapsedTime < AnimateSpeed)
        {
            elapsedTime += Time.deltaTime;
            currentEndPos = Vector3.Lerp(currentEndPos, fullEndPos, AnimateSpeed);
            LineRenderer.SetPosition(1, currentEndPos);
            yield return null;
        }

        DeactivateRender();

    }

    public void DeactivateRender()
    {
        isLineDrawing = false;
        LineRenderer.enabled = false;
    }



	// Update is called once per frame
	void Update () {
	    if (!isLineDrawing && Input.GetKeyDown(KeyCode.T))
	    {
	        StartCoroutine(StartLineDraw());
	    }
	}
}
