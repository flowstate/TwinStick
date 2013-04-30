using UnityEngine;
using System.Collections;

public class GrowAndDestroy : MonoBehaviour {
	
	public float initialRadius = 1;
	public float finalRadius = 5;
	public float growTime = 1;
	Transform _transform;
	Renderer _renderer;
	Vector3 initialScale, targetScale;
	Color _color;
	// Use this for initialization
	void Start () {
		_transform = transform;
		_renderer = renderer;
		_color = _renderer.material.color;
		SetScales();
		StartCoroutine(GrowAndKill());
	}
	
	IEnumerator GrowAndKill(){
		float elapsedTime = 0.0f;
		
		while(elapsedTime <= growTime){
			elapsedTime += Time.deltaTime;
			_transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / growTime);
			_color.a = Mathf.Lerp(255,0,elapsedTime / growTime);	
			_renderer.material.color = _color;
			yield return null;
		}
		
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SetScales(){
		initialScale = new Vector3(initialRadius,initialRadius, initialRadius);
		targetScale = new Vector3(finalRadius,finalRadius, finalRadius);
	}
	
	
}
