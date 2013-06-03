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
                    doFling = true;
                }
            }
        }
        else
        {

        }

    }

    // done in lateUpdate to track parent's movements during the Update call
    void LateUpdate()
    {
        if (captive != null)
        {
            captive.localPosition = Vector3.zero;
            captive.localRotation = Quaternion.identity;

        }
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
                col.transform.parent = _transform;
                captive = col.transform;
                SetCaptured(true);

                if (IsInLayerMask(col.gameObject, stateful))
                {
                    Debug.Log("It's an enemy we captured!");
                    Enemy enemy = col.gameObject.GetComponent<Enemy>();
                    enemy.currentState = EnemyStates.CAPTURED;
                }
                captive.gameObject.layer = LayerMask.NameToLayer("PlayerBullets");
                isActive = false;
                //StartCoroutine(BringToTheFold(col.transform));
            }
        }


    }

    void Fling()
    {
        if (IsInLayerMask(captive.gameObject, stateful))
        {
            Debug.Log("FLUNG THE ENEMY!");
            Enemy enemy = captive.GetComponent<Enemy>();
            enemy.currentState = EnemyStates.FLUNG;
        }
        captive.parent = null;
        captive.rigidbody.AddExplosionForce(1000, _transform.parent.transform.position, 10);
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
        isActive = false;
        float elapsedTime = 0.0f;

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