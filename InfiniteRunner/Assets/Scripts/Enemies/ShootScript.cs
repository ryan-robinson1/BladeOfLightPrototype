using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float followSpeed = 10;
    [SerializeField]
    private Transform torso;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform legs;

    private float _torsoMoveSpeed;
    private float _headMoveSpeed;
    private float _legMoveSpeed;

    private float _moveBackwardDistance; 
    private void Start()
    {
        _torsoMoveSpeed = 20;
        _headMoveSpeed = 45;
        _legMoveSpeed = 15;


        if(torso == null)
            Debug.Log("Did not assign torso in hierarchy");
        if (head == null)
            Debug.Log("Did not assign head in hierarchy");
        if (legs == null)
            Debug.Log("Did not assign legs in hierarchy");


        _moveBackwardDistance = Random.Range(18, 25);
        _rb = this.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        LookAtTarget();
        MoveBackwards();
    }
    public void LookAtTarget()
    {
        Vector3 _lookDirection = objectToFollow.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);


        torso.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _torsoMoveSpeed * Time.deltaTime);
        head.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _headMoveSpeed * Time.deltaTime);
        legs.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _legMoveSpeed * Time.deltaTime);
    }
    public void MoveBackwards()
    {
        float positionDifference = transform.position.x - objectToFollow.position.x;
        if (positionDifference < _moveBackwardDistance && positionDifference>0)
        {
            _rb.velocity = new Vector3(transform.forward.x * -3f, 0, transform.forward.z * -(5f/positionDifference));
        }
    }

}
