using UnityEngine;

public class TurretShootScript : MonoBehaviour
{
    public GameObject bullet;

    private PlayerController player;
    private Transform objectToFollow;
    public GameObject hero;
    public Transform leftGunBarrel;
    public Transform rightGunBarrel;
    public Transform midConnector;
    private Rigidbody _rb;


    private float _bulletSpeed = 20f;
    private float _removalDistance = 10;
    private float positionDifference;


    private float _connectorMoveSpeed;



    private float ammo = 5;
    private float timeInBetweenShots = 0.85f;
    private float reloadTime = 3f;
    private float reloadTimer = float.NegativeInfinity;
    private bool shooting = false;
    private bool rightShot = false;

    Quaternion _lookRotation;
    Vector3 _direction;
    Animator _anim;
    ParticleSystem _muzzleFlash;
    private ParticleSystem.MainModule _mfMain;


    private void Start()
    {

        _connectorMoveSpeed = 40;

        _rb = this.GetComponent<Rigidbody>();
        // _anim = this.GetComponentInChildren<Animator>();
        //  _muzzleFlash = this.GetComponentInChildren<ParticleSystem>();

        player = hero.GetComponent<PlayerController>();
        objectToFollow = hero.transform.GetChild(1).transform;

        // muzzle flash color
        // _mfMain = _muzzleFlash.main;
        // _mfMain.startColor = ColorDataBase.GetEnemyStripAlbedo();
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
        else
        {
           // _anim.SetBool("shooting", false);
        }

        destroyModel();
        LookAtTarget();



    }
    /**
    *  Rotates the object to face the lookat point
    */
    private void LookAtTarget()
    {

        Vector3 _lookDirection = objectToFollow.position - transform.position;
        _lookDirection.y = 0;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);

        midConnector.transform.rotation = Quaternion.Lerp(
            transform.rotation, _rot, _connectorMoveSpeed * Time.time);

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
                spawnPos = leftGunBarrel.transform.position + leftGunBarrel.transform.forward;
                rightShot = false;
            }
            // otherwise, shoot from the right canister
            else
            {
                spawnPos = leftGunBarrel.transform.position + leftGunBarrel.transform.forward;
                rightShot = true;
            }
            

            //Sets the bullet speed in the script
            GameObject b = Instantiate(bullet, spawnPos, _lookRotation);
            b.GetComponent<BulletScript>().bulletSpeed = _bulletSpeed;
            //this.spawnBulletCasing();
           // _anim.SetBool("shooting", true);
           // _muzzleFlash.Play();
            //ammo--;
        }
        else if (ammo == 0)
        {
            //_anim.SetBool("shooting", false);
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
