using UnityEngine;
using System.Collections;

public class BasicJoystickMove : MonoBehaviour {

    public float MoveSpeed = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }

    private void Move()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = input.normalized * MoveSpeed;

        transform.position += input;
    }
}
