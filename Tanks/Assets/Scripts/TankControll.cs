using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll : MonoBehaviour
{
    [Header("Track Movement")]
    public Rigidbody tankRigidbody = null;
    public float tankMoveSpeed = 5f;
    public float tankRotateSpeed = 3f;

    [Header("Turret Rotation")]
    public GameObject turretObject = null;
    public float turretRotateSpeed = 3f;

    [Header("Muzzle Elevation")]
    public GameObject muzzleObject = null;
    public float muzzleElevationSpeed = 3f;
    [Range(0f, 15f)] public float maxBotElevation = 10f;
    [Range(0f, -30f)] public float maxTopElevation = -20f;

    [Header("Camera")]
    public GameObject cameraObject = null;

    void Start()
    {
        if (tankRigidbody == null) tankRigidbody = GameObject.Find("Tank").GetComponent<Rigidbody>();
        if (turretObject == null) turretObject = GameObject.Find("Tank/turret");
        if (muzzleObject == null) muzzleObject = GameObject.Find("Tank/turret/muzzle");
        if (cameraObject == null) cameraObject = GameObject.Find("Tank/fpp");
    }

    void FixedUpdate()
    {
        MoveTank(Input.GetAxis("Vertical"));
        RotateTank(Input.GetAxis("Horizontal"));

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            RotateTurret(GetControllDirection(turretObject.transform.localEulerAngles.y, cameraObject.transform.localEulerAngles.y));
            ElevateMuzzle(GetControllDirection(muzzleObject.transform.localEulerAngles.x, cameraObject.transform.localEulerAngles.x));
        }
        
    }

    float GetControllDirection(float ang1, float ang2)
    {
        float angle = ang1 - ang2;

        if (angle > 180f) angle -= 360f;
        else if (angle < -180f) angle += 360f;

        return angle < 0f ? Mathf.Clamp(-angle, 0f, 1f) : Mathf.Clamp(-angle, -1f, 0f);
    }

    void MoveTank(float amount)
    {
        amount = Mathf.Clamp(amount, -1f, 1f);
        Vector3 mInput = tankRigidbody.transform.rotation * (Vector3.forward * amount);
        tankRigidbody.MovePosition(tankRigidbody.transform.position + mInput * Time.fixedDeltaTime * tankMoveSpeed);
    }

    void RotateTank(float amount)
    {
        amount = Mathf.Clamp(amount, -1f, 1f);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, amount * 17f * tankRotateSpeed, 0f) * Time.fixedDeltaTime);
        tankRigidbody.MoveRotation(tankRigidbody.transform.rotation * deltaRotation);
    }

    void RotateTurret(float amount)
    {
        amount = Mathf.Clamp(amount, -1f, 1f);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, amount * 7f * turretRotateSpeed, 0f) * Time.fixedDeltaTime);
        turretObject.transform.rotation *= deltaRotation;
    }

    void ElevateMuzzle(float amount)
    {
        amount = Mathf.Clamp(amount, -1f, 1f);
        amount *= muzzleElevationSpeed * 3f * Time.fixedDeltaTime;

        float currentPitch = muzzleObject.transform.localEulerAngles.x;
        float newPitch = (currentPitch > 180f ? currentPitch - 360f : currentPitch) + amount;

        newPitch = Mathf.Clamp(newPitch, maxTopElevation, maxBotElevation);
        muzzleObject.transform.localEulerAngles = new Vector3(newPitch < 0 ? newPitch + 360f : newPitch, 0f, 0f);
    }

}