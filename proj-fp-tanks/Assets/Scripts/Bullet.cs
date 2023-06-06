using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 0f;
    public Vector3 direction = Vector3.zero;
    public TankControll tc;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -100f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Collider collider = other.collider;
        if (collider != null)
        {
            bool objectDestroyed = false;
            GameObject parent = collider.gameObject;
            while (parent != null)
            {
                if (parent.CompareTag("Enemy"))
                {
                    Destroy(parent);
                    objectDestroyed = true;
                    break;
                }
                parent = parent.transform.parent?.gameObject;
            }
            if (objectDestroyed) tc.score++;
            Destroy(gameObject);
        }
    }
}
