using UnityEngine;
using System.Collections;

public class ShootForward : MonoBehaviour {
	
	public GameObject bullet;
	public float bulletSpeed = 5f;
	public float shootDelay = 1.5f;
    private Vector3 shotScale, originalScale;
	protected Transform _transform;
	protected Vector3 shootDirection;
    private SceneManager sceneManager = null;
    public bool StartAutomatically = true;
    protected bool isFiring = false;
    public int maxBullets = -1;
    public bool ScaledByManager = false;
    [HideInInspector] public bool clickClick = false;
	
    
    protected void Start ()
	{
	    originalScale = shotScale = bullet.transform.localScale;
        
	    sceneManager = SceneManager.Instance;
        if (ScaledByManager)
        {
            shotScale = originalScale * sceneManager.ShotScale;
        }
		_transform = transform;
        if (StartAutomatically)
        {
            StartTimedShoot();
        }
		
	}

    public void StartTimedShoot()
    {
        if (!isFiring)
        {
            StartCoroutine("TimedShoot");
            isFiring = true;
          
        }
        
    }

    public void StopTimedShoot()
    {
        if (isFiring)
        {
            StopCoroutine("TimedShoot");
          
            isFiring = false;
        }
    }

	IEnumerator TimedShoot()
	{
	    int currentBullets = maxBullets;
	    
        bool oneInTheClip = true;

        while(oneInTheClip){
			
            Fire();
            if (maxBullets != -1)
            {
                currentBullets--;
                if (currentBullets == 0)
                {
                    oneInTheClip = false;
                    clickClick = true;
                }
            }
            yield return new WaitForSeconds(shootDelay);
		}
	}
	
	
   public virtual void Fire(){
		// direction that object is facing in local Z direction

        //if (ScaledByManager)
        //{
        //    //shotScale = sceneManager.ShotScaleVector;
        //    shotScale = originalScale*sceneManager.ShotScale;
        //}
        //else
        //{
        //    shotScale = originalScale;
        //}

		shootDirection = _transform.forward;
		GameObject bulletInstance = Instantiate (bullet, _transform.position, Quaternion.LookRotation(shootDirection)) as GameObject;
	    bulletInstance.transform.localScale = shotScale;
        Bullet bulletClass = bulletInstance.GetComponent<Bullet>();

		//bulletClass.findPlayer = true;
		bulletInstance.rigidbody.AddForce(bulletSpeed*shootDirection, ForceMode.VelocityChange);
		
	}
}
