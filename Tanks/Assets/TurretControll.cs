using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TurretControll : MonoBehaviour
{

    public GameObject muzzle = null;
    public float rotateSpeed = 4f;
    public float muzzleSpeed = 3f;
    public float maxAngleTop = 30f;
    public float maxAngleBottom = 10f;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rotateTurret();
        rotateMuzzle();
    }

    void rotateTurret()
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, Input.GetAxis("Horizontal") * 50 * rotateSpeed / 3f, 0) * Time.fixedDeltaTime);
        transform.rotation *= deltaRotation;
    }

    void rotateMuzzle()
    {
        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(Input.GetAxis("Horizontal") * 50 * muzzleSpeed / 3f, 0, 0) * Time.fixedDeltaTime);
        //if (deltaRotation.x <= maxAngleBottom && deltaRotation.x >= -maxAngleTop)
        //muzzle.transform.rotation *= deltaRotation;
    ///TODO NOT WORKING ELEVATION
        Quaternion elev = Quaternion.Euler(10f, 0, 0);
        muzzle.transform.localRotation = Quaternion.Lerp(muzzle.transform.localRotation, elev, Input.GetAxis("Horizontal") * 50 * muzzleSpeed / 3f * Time.fixedDeltaTime);

    }
}
