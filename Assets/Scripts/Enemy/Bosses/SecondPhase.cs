using UnityEngine;
using System.Collections;

public class SecondPhase : MonoBehaviour
{


    private Hashtable inTable, outTable;
    public iTween.EaseType Ease;
    public float AnimTime;
    public float FiringTimer;
    private Rigidbody _rigidbody;
    private bool _movingRight, _isShooting;
    public float MoveSpeed;
    public LayerMask FlipMask;
    private Transform _transform;
    private Vector3 _motion;
    private Vector3 targetScale, originalScale;
    public FiringCircle FiringCircle;
    public LayerMask HitMask;
    private int health = 3;

	// Use this for initialization
	void Start ()
	{
	    _transform = transform;
	    _isShooting = false;
	    _movingRight = true;
	    _rigidbody = rigidbody;
        _motion = new Vector3(MoveSpeed, 0, 0);
	    originalScale = _transform.localScale;
        targetScale = new Vector3(0.85f,0.85f,0.85f);
	    InitTable();
        StartCoroutine(TimedShot());
    }

    private void InitTable()
    {
        
        inTable = new Hashtable();
        inTable.Add("name", "inAnim");
        inTable.Add("time", AnimTime /2f);
        inTable.Add("easetype", iTween.EaseType.linear);
        inTable.Add("oncomplete", "BeginOut");
        inTable.Add("oncompletetarget", gameObject);
        inTable.Add("scale", targetScale);

        outTable = new Hashtable();
        outTable.Add("name", "outAnim");
        outTable.Add("time", AnimTime / 2f);
        outTable.Add("easetype", Ease);
        outTable.Add("oncomplete", "Fire");
        outTable.Add("oncompletetarget", gameObject);
        outTable.Add("scale", originalScale);
    }

    IEnumerator TimedShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(FiringTimer);
            if (!_isShooting)
            {
                BeginShot();    
            }
            
        }
    }
   
    void BeginShot()
    {
        iTween.ScaleTo(gameObject, inTable);
        _isShooting = true;
    }
	
    void BeginOut()
    {
        iTween.ScaleTo(gameObject, outTable);

    }

    void Fire()
    {
        FiringCircle.Fire(0);
        _isShooting = false;
    }

	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (_movingRight)
        {
            _rigidbody.velocity = _motion;
        }
        else
        {
            _rigidbody.velocity = _motion*-1f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (Constants.IsInLayerMask(col.gameObject, FlipMask))
        {
            _movingRight = !_movingRight;
        }
        else if (Constants.IsInLayerMask(col.gameObject, HitMask))
        {
            TakeHit();
        }
    }

	void TakeHit()
	{
	    health--;
        if (health <= 0)
        {
            Destroy(gameObject);

        }
	}
}
