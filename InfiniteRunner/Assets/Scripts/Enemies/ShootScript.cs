﻿using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bullet;
    public PlayerController player;

    public BulletScript robots;
    
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

    [SerializeField]
    private float _bulletSpeed = 25f; 

    private float _walkSpeed = 3f;
    private float _removalDistance = 10;
  

    private float _torsoMoveSpeed;
    private float _headMoveSpeed;
    private float _legMoveSpeed;

    private float _moveBackwardDistance;

    private float positionDifference;

    private float ammo = 5;
    private float timeInBetweenShots = 0.5f;
    private float reloadTime = 1.8f;
    private float reloadTimer = float.NegativeInfinity;
    private bool shooting = false;

    Quaternion _lookRotation;
    Vector3 _direction;
    Animator _anim;
    ParticleSystem _muzzleFlash;

    private Rigidbody _rb;
    private void Start()
    {

        _torsoMoveSpeed = 50;
        _headMoveSpeed = 55;
        _legMoveSpeed = 40;


        if(torso == null)
            Debug.Log("Did not assign torso in hierarchy");
        if (head == null)
            Debug.Log("Did not assign head in hierarchy");
        if (legs == null)
            Debug.Log("Did not assign legs in hierarchy");


        _moveBackwardDistance = Random.Range(10,15);
        _rb = this.GetComponent<Rigidbody>();
        _anim = this.GetComponentInChildren<Animator>();
        _muzzleFlash = this.GetComponentInChildren<ParticleSystem>();
    }
    
    private void FixedUpdate()
    {
        positionDifference = transform.position.x - (objectToFollow.position.x);
        MoveBackwards();
        if(positionDifference < Random.Range(80, 10) && !shooting)
        {
            InvokeRepeating("Shoot", 0f, timeInBetweenShots);
            shooting = true;
        }
        else
        {
            _anim.SetBool("shooting", false);
        }

        destroyModel();
      //  LookAtTarget();



    }
    private void Update()
    {
        

    }
    private void LateUpdate()
    {
        _anim.enabled = true;
    }


    private void LookAtTarget()
    {
        Vector3 _lookDirection = objectToFollow.position - transform.position;
        _lookDirection.y = 0;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);

        _anim.enabled = false;
        torso.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _torsoMoveSpeed * Time.deltaTime);
        head.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _headMoveSpeed * Time.deltaTime);
        legs.transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _legMoveSpeed * Time.deltaTime);
        
    }
    
    private void MoveBackwards()
    {
   
        if (positionDifference < _moveBackwardDistance && positionDifference>0)
        {
            _rb.velocity = new Vector3(transform.forward.x * -_walkSpeed, 0, transform.forward.z * -(1f/positionDifference*2));
            _anim.SetTrigger("PlayWalkBackward");
        }
    }

    private void Shoot()
    {
        if (ammo>0 && Time.time-reloadTimer>reloadTime && positionDifference >0)
        {
            Vector3 _pointToAimAt = CalculateInterceptPosition(objectToFollow.position);
            _direction = (_pointToAimAt - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);


            Vector3 spawnPos = gunBarrel.transform.position + gunBarrel.transform.forward;

            //Sets the bullet speed in the script
            GameObject b = Instantiate(bullet, spawnPos, _lookRotation);
            b.GetComponent<BulletScript>().bulletSpeed = _bulletSpeed;
            _anim.SetBool("shooting", true);
            _muzzleFlash.Play();
            ammo--;
        }
        else if(ammo == 0)
        {
            _anim.SetBool("shooting", false);
            reloadTimer = Time.time;
            ammo = 5;
        }


    }

    private void destroyModel()
    {
        if (-positionDifference > _removalDistance)
        {
           Destroy(this.gameObject);
        }
    }
    private Vector3 CalculateInterceptPosition(Vector3 targetPosition)
    {
        Vector3 interceptPoint = FirstOrderIntercept(
            this.transform.position,_rb.velocity,_bulletSpeed,new Vector3(targetPosition.x,targetPosition.y-1f,targetPosition.z),player.getVelocity());
        Vector3 inaccurateInterceptPoint = new Vector3(Random.Range(targetPosition.x, interceptPoint.x), interceptPoint.y, Random.Range(targetPosition.z, interceptPoint.z));
        return inaccurateInterceptPoint;
    }
    public static Vector3 FirstOrderIntercept
    (
        Vector3 shooterPosition,
        Vector3 shooterVelocity,
        float shotSpeed,
        Vector3 targetPosition,
        Vector3 targetVelocity
    )
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;

       
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
       
        return targetPosition + t * (targetRelativeVelocity);
    }
    //Uses relative position to calculate the first intercept
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handles alike velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); 
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;
    
      
        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }


}
