using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody BallRigidBody;
    public float Speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        BallRigidBody = GetComponent<Rigidbody>();
        BallRigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            BallRigidBody.AddForce(transform.forward * Speed);
        }
        if(Input.GetKey(KeyCode.A))
        {
            BallRigidBody.AddForce(-transform.right * Speed);
        }
        if(Input.GetKey(KeyCode.S))
        {
            BallRigidBody.AddForce(-transform.forward * Speed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            BallRigidBody.AddForce(transform.right * Speed);
        }
    }
}
