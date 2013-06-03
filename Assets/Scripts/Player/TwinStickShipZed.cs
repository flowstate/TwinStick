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
    private Vector3 moveDirection, fireDirection;
    private float startingDrag;
    private Rigidbody _rigidbody;
    private UILabel moveSpeedText;
    private bool invincible;
    private float speedSquared;

    private float sqrStartDragVelocity,
                  sqrMaxDragVelocity,
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

        _transform = transform;
        _rigidbody = rigidbody;
        startingDrag = _rigidbody.drag;
        speedSquared = moveSpeed*moveSpeed;
        Initialize();

    }

    void Initialize()
    {
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

        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //rigidbody.MovePosition(_transform.position + moveDirection * Time.deltaTime * moveSpeed);
        CalculateDrag();
        //Move();



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
        //Vector3 joystickForce = new Vector3(Horizontal, 0f, Vertical);
        Vector3 joystickForce = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.AddForce(joystickForce.normalized * (speedSquared + speedSquared));

    }

    


    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.O)) {
            HitTaken();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Restart"))
        {
            Application.LoadLevel(0);
        }
    }

    void OnCollisionEnter()
    {
        _rigidbody.velocity = Vector3.zero;

        if (!invincible)
        {
            HitTaken();
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
}
