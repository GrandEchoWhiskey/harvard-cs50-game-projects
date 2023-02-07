using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public float move_Speed = 5f;
    public float rotate_Speed = 3f;

    void Start()
    {
        if(m_Rigidbody == null)
            m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        move();
        rotate();
    }

    void move()
    {
        Vector3 m_Input = transform.rotation * (Vector3.forward * Input.GetAxis("Vertical"));
        m_Rigidbody.MovePosition(transform.position + m_Input * Time.fixedDeltaTime * move_Speed);
    }

    void rotate()
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, Input.GetAxis("Horizontal")*50, 0) * Time.fixedDeltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
    }
}
