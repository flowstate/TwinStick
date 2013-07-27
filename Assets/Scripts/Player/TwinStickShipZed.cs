using UnityEngine;
using System.Collections;

public class TwinStickShipZed : MonoBehaviour
{
    public int currentHealth = 3;
    public int maxHealth = 3;
    public float moveSpeed = 10;
    public bool debug = false;
    public float maxDrag = 6;
    public UILabel HealthLabel;
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
    private Vector3 originalScale;
    public UILabel ScaleLabel;
    private Color originalColor;
    private Color invincibleColor = Color.magenta;
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
        invincible = false;
        canPlayerControl = true;
        _transform = transform;
        _rigidbody = rigidbody;
        startingDrag = _rigidbody.drag;
        speedSquared = moveSpeed*moveSpeed;
        originalScale = _transform.localScale;
        Initialize();
        OutputHealth();
    }

    private void OutputHealth()
    {
        if (HealthLabel != null)
        {
            HealthLabel.text = currentHealth.ToString("0");
            Debug.Log("OUTPUTTING HEALTH: " + currentHealth.ToString("0"));
        }
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
     
        CalculateDrag();

    }

    private void CalculateDrag()
    {
        // if the joysticks aren't dead
        if (Constants.IsLeftStickAlive(0.1f))
        {
            Move();

            float velSqr = _rigidbody.velocity.sqrMagnitude;

            // if the velocity is over the drag threshold
            if (velSqr > sqrStartDragVelocity)
            {
                _rigidbody.drag = Mathf.Lerp(startingDrag, maxDrag,
                                             Mathf.Clamp01((velSqr - sqrStartDragVelocity)/
                                                           sqrDragVelocityRange));

                // if velocity is over max velocity
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
        SetRotation();
        
    }

    private void SetRotation()
    {
        if (Constants.IsLeftStickAlive(0.1f))
        {
            Vector3 targetRotVector = Constants.GetLeftStickXZ().normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetRotVector);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, 1);
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

    void PlayerSliderChange(float val)
    {
        if (val > 0)
        {
            ScalePlayer(val*2f);
            
            if (ScaleLabel != null)
            {
                ScaleLabel.text = (val * 2f).ToString("0.00");

            }
        }
    }

    private void ScalePlayer(float val)
    {
        _transform.localScale = new Vector3(originalScale.x * val, originalScale.y, originalScale.z * val );
    }

    void HitTaken() {
        currentHealth--;
        OutputHealth();
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
        
        invincible = true;
        renderer.material.color = invincibleColor;
        yield return new WaitForSeconds(2.0f);
        
        invincible = false;
        renderer.material.color = originalColor;
        yield return null;
        
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
