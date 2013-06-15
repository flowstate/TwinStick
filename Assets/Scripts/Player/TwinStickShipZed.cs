using UnityEngine;
using System.Collections;

public class TwinStickShipZed : MonoBehaviour
{
    public int currentHealth = 3;
    public int maxHealth = 3;
    public float moveSpeed = 10;
    public bool debug = false;
    public float maxDrag = 3;
    public GameObject mSpeed;
    public float startDragVelocity = 10f;
    public float maxDragVelocity = 15f;
    public float maxTotalVelocity = 20f;
    private Transform _transform;
    private float startingDrag;
    private Rigidbody _rigidbody;
    private UILabel moveSpeedText;
    private bool invincible, canPlayerControl;
    private float speedSquared;
    private Hashtable tweenTable;
    public LayerMask HitMask;

    private float sqrStartDragVelocity,
                  sqrDragVelocityRange,
                  sqrMaxVelocity;

    [HideInInspector]
    public bool hasCaptured = false;
    

    float Horizontal {
        get { return Input.GetAxis("Horizontal"); }
    }

    float Vertical
    {
        get { return Input.GetAxis("Vertical"); }
    }


    // Use this for initialization
    void Start()
    {

        canPlayerControl = true;
        _transform = transform;
        _rigidbody = rigidbody;
        startingDrag = _rigidbody.drag;
        speedSquared = moveSpeed*moveSpeed;
        Initialize();

    }

    void Initialize()
    {
        tweenTable = new Hashtable();
        tweenTable.Add("x", 0f);
        tweenTable.Add("z", -42f);
        tweenTable.Add("time", 3f);
        tweenTable.Add("easetype", iTween.EaseType.linear);
        tweenTable.Add("oncomplete", "ResumePlayerControl");
        tweenTable.Add("oncompletetarget", gameObject);
        tweenTable.Add("onstart", "DisablePlayerControl");
        tweenTable.Add("onstarttarget", gameObject);
        sqrStartDragVelocity = startDragVelocity*startDragVelocity;
        sqrDragVelocityRange = (maxDragVelocity*maxDragVelocity) - sqrStartDragVelocity;
        sqrMaxVelocity = maxTotalVelocity*maxTotalVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (debug)
        {
            moveSpeedText.text = moveSpeed.ToString("00");

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveSpeed += 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveSpeed -= 1;
            }
        }
       
        CalculateDrag();

    }

    private void CalculateDrag()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) >= 0.1f)
        {
            Move();

            float velSqr = _rigidbody.velocity.sqrMagnitude;

            if (velSqr > sqrStartDragVelocity)
            {
                _rigidbody.drag = Mathf.Lerp(startingDrag, maxDrag,
                                             Mathf.Clamp01((velSqr - sqrStartDragVelocity)/
                                                           sqrDragVelocityRange));

                if (velSqr > sqrMaxVelocity)
                {
                    _rigidbody.velocity = _rigidbody.velocity.normalized*maxTotalVelocity;
                }
                
            }
            else
            {
                // normal drag and movement
                _rigidbody.drag = startingDrag;    
            }

            
        }
        else
        {
            // turn up drag to do some shit
            _rigidbody.drag = maxDrag;
        }
    }

    void Move()
    {
        
        // add force from the joystick
        if (canPlayerControl)
        {
            Vector3 joystickForce = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rigidbody.AddForce(joystickForce.normalized * (speedSquared + speedSquared));    
        }
        

    }

    
    public void MoveTween(Vector3 position, float time)
    {
        tweenTable["x"] = position.x;
        tweenTable["z"] = position.z;
        tweenTable["time"] = time;

        iTween.MoveTo(gameObject, tweenTable);
    }

    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.O)) {
            HitTaken();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.zero;

        if (!invincible)
        {
            if (Constants.IsInLayerMask(collision.gameObject, HitMask))
            {
                HitTaken();    
            }
            
        }
        
    }

    void HitTaken() {
        currentHealth--;
        if (currentHealth == 0)
        {
            Debug.Log("Oh man, I'm dead!");
            Destroy(gameObject);
        }

        else {
            StartCoroutine(InvincibilityFrames());
        }
    }

    IEnumerator InvincibilityFrames()
    {
        // make invincible
        Debug.Log("I'm invincible!!");
        invincible = true;
        yield return new WaitForSeconds(2.0f);
        Debug.Log("I guess now I'm ... vincible?");
        invincible = false;
        // yield return
        // make not invincible
    }

    public void ResumePlayerControl()
    {
        canPlayerControl = true;
    }

    public void DisablePlayerControl()
    {
        canPlayerControl = false;
    }
}
