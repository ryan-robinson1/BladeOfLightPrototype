using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bullet; 

    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private Transform gunBarrel;
    [SerializeField]
    private float followSpeed = 10;
    [SerializeField]
    private Transform torso;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform legs;
    //[SerializeField]
    private float walkSpeed = 0.5f;
    private float removalDistance = 10;
    Animator _anim;

    private float _torsoMoveSpeed;
    private float _headMoveSpeed;
    private float _legMoveSpeed;

    private float _moveBackwardDistance;

    Quaternion _lookRotation;
    Vector3 _direction;

    private Rigidbody _rb;
    private void Start()
    {
        _torsoMoveSpeed = 40;
        _headMoveSpeed = 45;
        _legMoveSpeed = 35;


        if(torso == null)
            Debug.Log("Did not assign torso in hierarchy");
        if (head == null)
            Debug.Log("Did not assign head in hierarchy");
        if (legs == null)
            Debug.Log("Did not assign legs in hierarchy");


        _moveBackwardDistance = Random.Range(50, 100);
        _rb = this.GetComponent<Rigidbody>();
        _anim = this.GetComponentInChildren<Animator>();
    }
    
    private void FixedUpdate()
    {

        LookAtTarget();
        MoveBackwards();
        if(Time.time%Random.Range(3,5)==0)
            Shoot();
        destroyModel();
    }
    private void LookAtTarget()
    {
        Vector3 _lookDirection = objectToFollow.position - transform.position;
        _lookDirection.y = 0;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);



        torso.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _torsoMoveSpeed * Time.deltaTime);
        head.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _headMoveSpeed * Time.deltaTime);
        legs.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _legMoveSpeed * Time.deltaTime);
   

    }
    private void MoveBackwards()
    {
        float positionDifference = transform.position.x - objectToFollow.position.x;
        if (positionDifference < _moveBackwardDistance && positionDifference>0)
        {
            _rb.velocity = new Vector3(transform.forward.x * -walkSpeed, 0, transform.forward.z * -(1f/positionDifference*2));
            _anim.SetTrigger("PlayWalkBackward");
        }
    }

    private void Shoot()
    {
        Vector3 _pointToAimAt = new Vector3(objectToFollow.position.x, objectToFollow.position.y, objectToFollow.position.z);
        _direction = (_pointToAimAt - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        


        Vector3 spawnPos = torso.transform.position + torso.transform.forward*1.1f;
        Instantiate(bullet, spawnPos, _lookRotation); 

    }

    private void destroyModel()
    {
        float positionDifference = objectToFollow.position.x - transform.position.x;
        if (positionDifference > removalDistance)
        {
            Destroy(this.gameObject);
        }
    }

}
