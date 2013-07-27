using UnityEngine;
using System.Collections;

public class TractorBeam : MonoBehaviour
{

    Transform _transform, captive;
    float foldTime = 1;
    bool isActive = true;
    bool doFling = false;
    TractorZed parentTractor;
    public LayerMask canCapture;
    public LayerMask stateful;
    public LayerMask HighlightMask;
    public QueueManager QManager;

    // Use this for initialization
    void Start()
    {
        _transform = transform;
        parentTractor = _transform.parent.GetComponent<TractorZed>();
    }

    public static bool IsInLayerMask(GameObject test, LayerMask against)
    {
        int objLayerMask = (1 << test.layer);
        return (against.value & objLayerMask) > 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
        
            if (captive == null)
            {
                isActive = true;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fling"))
                {
                    Debug.Log("PRESSED FLING!");
                    doFling = true;
                }
                
            }
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GetFromQueue();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SendToQueue();
        }

    }

    // done in lateUpdate to track parent's movements during the Update call
    void LateUpdate()
    {
        if (captive != null)
        {
            if (!parentTractor.IsAimSlowed)
            {
                CastTheRay();
            }
            captive.localPosition = Vector3.zero;
            captive.localRotation = Quaternion.identity;

        }

       
    }

    // send a captured enemy to the queue
    public void SendToQueue()
    {
        if (null != captive)
        {
            QManager.Push(captive.gameObject);
            CaptiveGone();
        }
    }

    public void GetFromQueue()
    {
        StartCoroutine(BringToTheFold(QManager.Pop().transform));
    }

    private void CastTheRay()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,
                             HighlightMask)) return;

        GameObject victim = hit.transform.gameObject;
        
        victim.SendMessage("Highlight", SendMessageOptions.DontRequireReceiver);


        StartCoroutine(parentTractor.FreezeAim());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            return;
        }

        // if it's something we can capture
        if (IsInLayerMask(col.gameObject, canCapture))
        {
            if (isActive)
            {
                CaptureEnemy(col.gameObject);
            }
        }


    }

    private void CaptureEnemy(GameObject shawshank)
    {
        shawshank.transform.parent = _transform;
        captive = shawshank.transform;
        SetCaptured(true);

        if (Constants.IsInLayerMask(shawshank, stateful))
        {
            Enemy enemy = shawshank.GetComponent<Enemy>();
            enemy.currentState = EnemyStates.CAPTURED;
        }

        shawshank.layer = LayerMask.NameToLayer("PlayerBullets");
        isActive = false;
    }

    void Fling()
    {
        captive.rigidbody.velocity = Vector3.zero;

        if (IsInLayerMask(captive.gameObject, stateful))
        {
            Enemy enemy = captive.GetComponent<Enemy>();
            enemy.currentState = EnemyStates.FLUNG;
        }

        captive.rigidbody.AddExplosionForce(1000, _transform.parent.transform.position, 10);
        CaptiveGone();
        
    }

    void CaptiveGone()
    {
        captive.parent = null;

        captive = null;
        SetCaptured(false);
        isActive = true;
    }

    void FixedUpdate()
    {
        if (captive)
        {
            if (doFling)
            {
                Fling();
                doFling = false;
            }
            else
            {
                captive.rigidbody.velocity = Vector3.zero;
            }

        }
    }

    IEnumerator ActivateDelay()
    {
        yield return new WaitForSeconds(.5f);
        isActive = true;
    }

    IEnumerator BringToTheFold(Transform sheep)
    {
        captive = sheep;
        isActive = false;
        Debug.Log("Set active to false");
        float elapsedTime = 0.0f;
        sheep.parent = _transform;
        while (elapsedTime <= foldTime)
        {
            elapsedTime += Time.deltaTime;
            // set position
            sheep.position = Vector3.Slerp(sheep.position, _transform.position, elapsedTime / foldTime);
            sheep.rotation = Quaternion.Slerp(sheep.rotation, _transform.rotation, elapsedTime / foldTime);

            yield return null;
        }

    }

    void SetCaptured(bool value)
    {
        parentTractor.SetCaptured(value);
    }

}
