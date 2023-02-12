using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class VehicleControll : MonoBehaviour
{
    public Rigidbody mRigidbody;
    public float moveSpeed = 5f;
    public float rotateSpeed = 3f;

    void Start()
    {
        if(mRigidbody == null) mRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        move();
        rotate();
    }

    void move()
    {
        Vector3 mInput = transform.rotation * (Vector3.forward * Input.GetAxis("Vertical"));
        mRigidbody.MovePosition(transform.position + mInput * Time.fixedDeltaTime * moveSpeed);
    }

    void rotate()
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, Input.GetAxis("Horizontal") * 17 * rotateSpeed, 0) * Time.fixedDeltaTime);
        mRigidbody.MoveRotation(mRigidbody.rotation * deltaRotation);
    }
}
