using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public void LookAtTarget()
    {
        Vector3 _lookDirection = objectToFollow.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
    }
    public void MoveToTarget()
    {
        Vector3 _targetPos = new Vector3(objectToFollow.position.x,objectToFollow.position.y,0) +
                             objectToFollow.forward * offset.z +
                             objectToFollow.right * offset.x +
                             objectToFollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }
    public Transform objectToFollow;
    public Vector3 offset;
    public float followSpeed = 10;
    public float lookSpeed = 10;

}


