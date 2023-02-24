using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll : MonoBehaviour
{
    [Header("Tracks")]
    public Rigidbody tankRigidbody = null;
    public float tankMoveSpeed = 5f;
    public float tankRotateSpeed = 3f;

    [Header("Turret")]
    public GameObject turretObject = null;
    public float turretRotateSpeed = 3f;

    [Header("Muzzle")]
    public GameObject muzzleObject = null;
    public float muzzleElevationSpeed = 3f;
    [Range(0f, 15f)] public float maxBotElevation = 10f;
    [Range(0f, -30f)] public float maxTopElevation = -20f;

    [Header("Shooting")]
    public GameObject bulletSpawnerObject = null;
    public GameObject bulletPrefab = null;

    GameObject lastBullet;

    [Header("Camera")]
    public GameObject fppObject = null;

    public CameraControll script = null;

    void Start()
    {
        if (tankRigidbody == null) tankRigidbody = GameObject.Find("Tank").GetComponent<Rigidbody>();
        if (turretObject == null) turretObject = GameObject.Find("Tank/turret");
        if (muzzleObject == null) muzzleObject = GameObject.Find("Tank/turret/muzzle");
        if (bulletSpawnerObject == null) bulletSpawnerObject = GameObject.Find("Tank/turret/muzzle/BulletSpawner");
        if (fppObject == null) fppObject = GameObject.Find("Tank/fpp");
        if (script == null) script = GameObject.Find("Tank").GetComponent<CameraControll>();
        if (bulletPrefab == null) bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    void FixedUpdate()
    {
        MoveTank(Input.GetAxis("Vertical"));
        RotateTank(Input.GetAxis("Horizontal"));

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            RotateTurret(GetControllDirection(turretObject.transform.localEulerAngles.y, fppObject.transform.localEulerAngles.y));
            ElevateMuzzle(GetControllDirection(muzzleObject.transform.localEulerAngles.x, CalculateXAngle(script.firstPerson)));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        lastBullet = Instantiate(bulletPrefab, bulletSpawnerObject.transform.position, bulletSpawnerObject.transform.rotation);
    }

    float CalculateXAngle(bool isFirst)
    {
        if (isFirst) return fppObject.transform.localEulerAngles.x;
        return fppObject.transform.localEulerAngles.x - 25;
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
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, amount * 9f * turretRotateSpeed, 0f) * Time.fixedDeltaTime);
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