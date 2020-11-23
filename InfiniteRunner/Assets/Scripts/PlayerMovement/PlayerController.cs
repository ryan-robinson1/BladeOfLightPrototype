using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{

    private PlayerAnimationController _animController;

    public TimeManagerScript timeManager;
    public Image StaminaBar;

    [SerializeField]
    private ParticleSystem hitEffect;
    [SerializeField]
    private Transform lookAtPoint;
   
    [HideInInspector]
    public bool deflecting;

    Rigidbody _rb;
    Transform _tf;
    BoxCollider _bc;
    public AudioSource _ac;

    private int _playerPositionOffset = 0;
    private float speed = 15f;


    public static AudioClip sliceSound;
   

    

    string[] recentInputs = new string[2];

    //Deflect timers
    private float deflectLength = 0.5f;
    private float deflectTimer = float.PositiveInfinity;
    private float staminaRefreshLength = 1f;
    private float staminaRefreshTimer = float.NegativeInfinity;

    private void Start()
    {
        _animController = this.GetComponent<PlayerAnimationController>();
        _rb = this.GetComponent<Rigidbody>();
        _tf = this.GetComponent<Transform>();
        _bc = this.GetComponent<BoxCollider>();
        _rb.velocity = new Vector3(speed, 0, 0);
      
       

        
    }
    public Vector3 getVelocity() { return new Vector3(speed,0,0); }

    private void Update()
    {
        Move();
        ChangeLookAtPoint();
        UpdateRecentInputs();
        Deflect();
       // Debug.Log(SwipeInput.Instance.Tap);
       // (SwipeInput.Instance.DoubleTap && TwoRecentTaps()
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeManager.SlowMotion();
            recentInputs[0] = "";
            recentInputs[1] = "";
        }
    }
    void ChangeLookAtPoint()
    {
       if (_animController.IsRunning())
        {
            lookAtPoint.localPosition = new Vector3(0, 0.4f, 0);
            _bc.size = new Vector3(_bc.size.x, 1.731701f, _bc.size.z);
            _bc.center = new Vector3(_bc.center.x, 0, _bc.center.z);
        }
        else if (_animController.IsSliding())
        {
            lookAtPoint.localPosition = new Vector3(0, -0.4f, 0);
            _bc.size = new Vector3(_bc.size.x, 0.4f, _bc.size.z);
            _bc.center = new Vector3(_bc.center.x, -0.4f, _bc.center.z);
        }
    }
    void Move()
    {
        _rb.velocity = new Vector3(speed, 0, 0);

        if ((Input.GetKeyDown(KeyCode.A) || SwipeInput.Instance.SwipeLeft)&& _playerPositionOffset < 2)
        {
            _tf.position = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z + 1.75f);
            _playerPositionOffset += 1;
        }
        if ((Input.GetKeyDown(KeyCode.D) || SwipeInput.Instance.SwipeRight) && _playerPositionOffset > -2)
        {
            _tf.position = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z - 1.75f);
            _playerPositionOffset -= 1;
        }

    }
    //Sets the deflecting variable to true when the user taps the screen
    void Deflect()
    {
        if(Time.time-staminaRefreshTimer >= staminaRefreshLength)
        {
            if (Time.time - deflectTimer >= deflectLength)
            {
                deflecting = false;
                deflectTimer = float.PositiveInfinity;
                StaminaBar.fillAmount = 0;
                staminaRefreshTimer = Time.time;


            }
            else if ((SwipeInput.Instance.Tap || Input.GetKeyDown(KeyCode.W) )&& !deflecting)
            {
                deflecting = true;
                deflectTimer = Time.time;

            }
            else if (deflectTimer != float.PositiveInfinity)
            {
                StaminaBar.fillAmount = 1 - ((Time.time - deflectTimer) * 2);
            }
        }
        else
        {
           StaminaBar.fillAmount = ((Time.time - staminaRefreshTimer));
        }
       
     
    }
    //Plays the hit particle system. Activated by BulletScript
    public void PlayHitEffect()
    {
        _ac.pitch = Random.Range(1.0f, 1.25f);
        _ac.Play();
        Debug.Log("PLAY");
        hitEffect.Play();
    }

    /* Even though I wrote them, I'm not completely sure why these functions work, but their purpose is to log 
    /* the two most recent inputs to prevent a double swipe from being counted as a double tap
    */
    void UpdateRecentInputs()
    {
        if(SwipeInput.Instance.SwipeRight || SwipeInput.Instance.SwipeLeft || SwipeInput.Instance.SwipeUp || SwipeInput.Instance.SwipeDown)
        {
            if(recentInputs[0] == "Swipe")
            {
                recentInputs[1] = "Swipe";
            }
            else
            {
                recentInputs[0] = "Swipe";
            }
        }
        else if (SwipeInput.Instance.Tap)
        {
            if (recentInputs[0] == "Tap")
            {
                recentInputs[1] = "Tap";
            }
            else
            {
                recentInputs[0] = "Tap";
            }
        }
    }
    bool TwoRecentTaps()
    {
        return recentInputs[0] == "Tap" && recentInputs[1] == "Tap";
    }
   

}
