using UnityEngine;
using System.Collections;

public class TriggeredShootForward : MonoBehaviour {

    public GameObject bullet;
    public float bulletSpeed = 5f;
    public float shootDelay = 1.5f;
    public bool FireOnce = true;

    private Vector3 shotScale, originalScale;
    
    protected Transform _transform;
    protected Vector3 shootDirection;
    
    private SceneManager sceneManager = null;

    protected bool isFiring = false;
    
    public bool ScaledByManager = false;
    
    

	// Use this for initialization
	void Start () {
	    
	}

	// Update is called once per frame
	void Update () {
	
	}
}
