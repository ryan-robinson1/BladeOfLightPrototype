using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private float followSpeed = 10;
    [SerializeField]
    private Transform torso;
    [SerializeField]
    private Transform head;


    private float torsoMoveSpeed;
    private float headMoveSpeed;
    private void Start()
    {
        torsoMoveSpeed = 20;
        headMoveSpeed = 28;
        if(torso == null)
            Debug.Log("Did not assign torso in hierarchy");
        if (head == null)
            Debug.Log("Did not assign head in hierarchy");

    }
    private void FixedUpdate()
    {
        LookAtTarget();
    }
    public void LookAtTarget()
    {
        Vector3 _lookDirection = objectToFollow.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);

        torso.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, torsoMoveSpeed * Time.deltaTime);
        head.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, headMoveSpeed * Time.deltaTime);
    }
   
}
