﻿using UnityEngine;

public class TurretShootScript : MonoBehaviour
{
    public GameObject bullet;

    private PlayerController player;
    private Transform objectToFollow;
    private GameObject hero;
    public Transform leftGunBarrel;
    public Transform rightGunBarrel;
    private Rigidbody _rb;


    private float _bulletSpeed = 25f;
    private float _removalDistance = 10;
    private float positionDifference;



    private float ammo = 100;
    private float timeInBetweenShots = 0.35f;
    private float reloadTime = 3f;
    private float reloadTimer = float.NegativeInfinity;
    private bool shooting = false;
    private bool rightShot = false;

    Quaternion _lookRotation;
    Vector3 _direction;
    private Animator _ac;
    public ParticleSystem _leftFlash;
    public ParticleSystem _rightFlash;
    private ParticleSystem.MainModule _leftMain;
    private ParticleSystem.MainModule _rightMain;


    private void Start()
    {

        _rb = this.GetComponent<Rigidbody>();
        _ac = this.GetComponentInChildren<Animator>();
        hero = GameObject.FindGameObjectWithTag("Player");
        player = hero.GetComponent<PlayerController>();
        objectToFollow = hero.transform.GetChild(1).transform;

        SetMuzzleFlash();
    }

    /**
     * Sets the color of our muzzle flash particle systems.
     */
    private void SetMuzzleFlash()
    {
        // muzzle flash color
        _leftMain = _leftFlash.main;
        _leftMain.startColor = ColorDataBase.GetEnemyStripAlbedo();
        _rightMain = _rightFlash.main;
        _rightMain.startColor = ColorDataBase.GetEnemyStripAlbedo();
    }

    /**
     * Gets the hero object that this script is looking at.
     * 
     * @return The hero object this script is looking at.
     */
    public Transform GetHeroRef()
    {
        return objectToFollow;
    }


    /**
    *  Called every fixed frame rate (used bc of physics calculations)
    */
    private void FixedUpdate()
    {
        positionDifference = transform.position.x - (objectToFollow.position.x);
        if (positionDifference < Random.Range(80, 10) && !shooting)
        {
            InvokeRepeating("Shoot", 0f, timeInBetweenShots);
            shooting = true;
        }

        destroyModel();

    }

    /**
     *  Damages the player upon colliding with the player model.
     *  Destroys this turret object in the process and prompts it
     *  to explode.
     *  
     *  @param other The other collider
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DamageManager>().TakeDamageFromTurret();

            Destroy(this.gameObject);
        }

    }

    /**
     *  Shoots the gun at the predicted point returned by CalculateInterceptPosition. Is modified by 'ammo,' 'reloadTime,' and 'timeInBetweenShots.'
     */
    private void Shoot()
    {
        if (ammo > 0 && Time.time - reloadTimer > reloadTime + Random.Range(0, 0.4f) && positionDifference > 6)
        {
            Vector3 _pointToAimAt = CalculateInterceptPosition(objectToFollow.position);
            _direction = (_pointToAimAt - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);

            RaycastHit hit;

            Vector3 spawnPos = Vector3.zero;

            // if the right canister has just shot, shoot from the left canister
            if (rightShot)
            {
                spawnPos = leftGunBarrel.transform.position;
                _ac.SetTrigger("leftShoot");
                _leftFlash.Play();
                rightShot = false;
            }
            // otherwise, shoot from the right canister
            else
            {
                spawnPos = rightGunBarrel.transform.position;
                _ac.SetTrigger("rightShoot");
                _rightFlash.Play();
                rightShot = true;
            }


            //Sets the bullet speed in the script
            GameObject b = Instantiate(bullet, spawnPos, _lookRotation);
            b.GetComponent<BulletScript>().bulletSpeed = _bulletSpeed;
            // _muzzleFlash.Play();
            //ammo--;
        }
        else if (ammo == 0)
        {
            reloadTimer = Time.time;
            ammo = 5;
        }


    }

    /**
     * Destroys the enemy model after the player has passed it. 
     */
    private void destroyModel()
    {
        if (-positionDifference > _removalDistance)
        {
            player.GetComponent<PlayerAnimationController>().resetAttackMultiplier();
            Destroy(this.gameObject);
        }
    }
    /**
    *  Helper method that calculates the point where the enemy should shoot at
    *  
    *  @param targetPosition The current position of the target
    *  
    *  @return A random vector3 in between the target position and the predicted intercept position. Accuracy of shot can be modified in inaccurateInterceptPoint
    */
    private Vector3 CalculateInterceptPosition(Vector3 targetPosition)
    {
        Vector3 interceptPoint = ShootScript.FirstOrderIntercept(
            this.transform.position, _rb.velocity, _bulletSpeed, new Vector3(targetPosition.x, targetPosition.y - 1.5f, targetPosition.z), player.getVelocity());
        Vector3 inaccurateInterceptPoint = new Vector3(Random.Range(targetPosition.x, interceptPoint.x), interceptPoint.y, Random.Range(targetPosition.z, interceptPoint.z));

        if (player.timeSinceLastDodge > 8)
        {
            return interceptPoint;
        }
        return inaccurateInterceptPoint;
    }
}