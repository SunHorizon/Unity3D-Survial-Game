using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {


    public float projectileFource;
    public float projectileLifeTime;
    Rigidbody rb;

    // Use this for initialization
    void Start () {

        if (projectileFource <= 0)
        {
            projectileFource = 30.0f;
        }
        rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        if (projectileLifeTime <= 0)
        {
            projectileLifeTime = 2.0f;
        }
        rb.AddForce(transform.forward * projectileFource, ForceMode.Impulse);
    }

    void Update()
    {
        projectileLifeTime -= Time.fixedDeltaTime;
        if(projectileLifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
