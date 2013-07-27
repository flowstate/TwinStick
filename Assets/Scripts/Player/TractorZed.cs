using System.Collections;
using UnityEngine;

public class TractorZed : MonoBehaviour
{
    private Transform _beamTransform;
    private Transform _transform;
    public bool captured = false;
    public LayerMask mask;
    public GameObject Ship;
    private Transform shipTransform;
    public float maxDistance = 15;
    private GameObject beam;
    public float minDistance = 2;
    private TwinStickShipZed parentShip;
    public float rotateSpeed = 60.0f;

    public bool IsAimSlowed = false;

    // Use this for initialization
    private void Start()
    {
        _transform = transform;
        _beamTransform = _transform.Find("TractorArea");
        beam = _beamTransform.gameObject;
        parentShip = Ship.GetComponent<TwinStickShipZed>();
        shipTransform = Ship.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        SetPosition();
        if (!IsAimSlowed)
        {
            SetRotation();
        }
        
    }

    private void SetPosition()
    {
        _transform.position = shipTransform.position;
    }

    private void SetRotation()
    {
        if (Constants.IsRightStickAlive(0.1f))
        {
            Vector3 targetRotVector = Constants.GetRightStickXZ().normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetRotVector);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, 1);
        }

        // if we don't have a captive, make the tractor... invisible?
        else
        {
            
        }
    }

    
    public IEnumerator FreezeAim()
    {

        IsAimSlowed = true;
        Vector2 initial = Constants.GetRightTwo();
        Vector2 current = initial;

        while (Mathf.Abs(Vector2.Angle(initial, current)) < 5f)
        {
            yield return null;

            current = Constants.GetRightTwo();
        }

        IsAimSlowed = false;
    }


    public void SetCaptured(bool value)
    {
        captured = value;
        parentShip.hasCaptured = value;
    }
}
