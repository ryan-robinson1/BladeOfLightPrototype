﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    private float lifeLength = 2f;
    private float lifeLengthTimer = float.PositiveInfinity;
    [HideInInspector]
    public float bulletSpeed;

    Rigidbody rb;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        lifeLengthTimer = Time.time;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(transform.forward.x * bulletSpeed, transform.forward.y*bulletSpeed, transform.forward.z * bulletSpeed);

        if (Time.time - lifeLengthTimer > lifeLength)
            Destroy(this.gameObject); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DamageManager>().TakeDamage();
            Debug.Log("Hit by bullet");
            Destroy(this.gameObject);

        }
    }
    public float getBulletSpeed()
    {
        return bulletSpeed; 
    }
}
