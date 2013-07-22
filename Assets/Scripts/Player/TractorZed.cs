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
        //parentShip = _transform.parent.GetComponent<TwinStickShipZed>();
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
        if (Input.GetAxis("FireHorizontal") != 0 || Input.GetAxis("FireVertical") != 0)
        {
            Vector3 targetRotVector =
                new Vector3(Input.GetAxis("FireHorizontal"), 0, Input.GetAxis("FireVertical")).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetRotVector);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, 1);
        }

        else
        {
            
        }
    }

    
    public IEnumerator FreezeAim()
    {

        IsAimSlowed = true;
        Vector2 initial = new Vector2(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"));
        Vector2 current = initial;

        while (Mathf.Abs(Vector2.Angle(initial, current)) < 5f)
        {
            yield return null;

            current = new Vector2(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"));
        }

        IsAimSlowed = false;
    }


    public void SetCaptured(bool value)
    {
        captured = value;
        parentShip.hasCaptured = value;
    }
}
