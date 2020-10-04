using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    private float lifeLength = 6f;
    private float lifeLengthTimer = float.PositiveInfinity;

    Rigidbody rb;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        lifeLengthTimer = Time.time;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(transform.forward.x * 25f, transform.forward.y*25f, transform.forward.z * 25f);

        if (Time.time - lifeLengthTimer > lifeLength)
            Destroy(this.gameObject); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            Debug.Log("Hit by bullet");
        }
    }
}
