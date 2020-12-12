using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{

    private PlayerAnimationController _animController;
    private BulletScript bScript;

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
    AudioManager _am;


    private int _playerPositionOffset = 0;
    private float speed = 15f;
    private int deflects = 0;


    Dictionary<int, float> positionMap =
    new Dictionary<int, float>();

   



    public List<Transform> deflectParticlePositions;





    string[] recentInputs = new string[2];

    //Deflect timers
    private float deflectLength = 2.5f; //1.5
    private float deflectTimer = float.PositiveInfinity;
    private float staminaRefreshLength = 4f;
    private float staminaRefreshTimer = float.NegativeInfinity;
    private float currentFillCapacity = 0f;

    //pitch timer
    private float pitchComboTimer = float.PositiveInfinity;
    private float deflectComboLength = 0f;
    private float currentPitch = 1f;

    private float transitionSpeed = 8f;



    private void Start()
    {
        _animController = this.GetComponent<PlayerAnimationController>();
        _rb = this.GetComponent<Rigidbody>();
        _tf = this.GetComponent<Transform>();
        _bc = this.GetComponent<BoxCollider>();
        _am = FindObjectOfType<AudioManager>();
        _rb.velocity = new Vector3(speed, 0, 0);

       
        positionMap[-1] = -1.75f;
        positionMap[-2] = -3.5f;
        positionMap[0] = 0f;
        positionMap[1] = 1.75f;
        positionMap[2] = 3.5f;

       



    }
    public Vector3 getVelocity() { return new Vector3(speed, 0, 0); }

    private void Update()
    {
        UpdateMove();
        ChangeLookAtPoint();
        resetCurrentPitch();
        Deflect();

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


        }
        else if (_animController.IsSliding())
        {
            lookAtPoint.localPosition = new Vector3(0, -0.4f, 0);
        }

        if (Input.GetKeyDown(KeyCode.S) || SwipeInput.Instance.SwipeDown)
        {
            _am.Pause("Footsteps");
            _bc.size = new Vector3(_bc.size.x, 0.4f, _bc.size.z);
            _bc.center = new Vector3(_bc.center.x, -0.4f, _bc.center.z);
            Invoke("resetBC", 1.27f);
        }
    }
    void resetBC()
    {
        _bc.size = new Vector3(_bc.size.x, 1.731701f, _bc.size.z);
        _bc.center = new Vector3(_bc.center.x, 0, _bc.center.z);
        _am.UnPause("Footsteps");
    }
    void UpdateMove()
    {
        _rb.velocity = new Vector3(speed, 0, 0);

        if ((Input.GetKeyDown(KeyCode.A) || SwipeInput.Instance.SwipeLeft))
        {
            if (_playerPositionOffset < 2)
            {
                _playerPositionOffset++;
            }
        }

        else if ((Input.GetKeyDown(KeyCode.D) || SwipeInput.Instance.SwipeRight))
        {
            if (_playerPositionOffset > -2)
            {
                _playerPositionOffset--;
            }
        }

        _tf.position = Vector3.Lerp(_tf.position, 
            new Vector3(_tf.position.x, _tf.position.y, 
            positionMap[_playerPositionOffset]), 
            Time.deltaTime * transitionSpeed);

    }
    

    /**
     * Gets the number of deflects in one deflect animation cycle.
     */

    public int getDeflects()
    {
        return deflects;
    }

    /**
     * Sets the number of deflects
     */
    public int setDeflects(int def)
    {
        deflects = def;
        return deflects;
    }

    //Sets the deflecting variable to true when the user taps the screen
    void Deflect()
    {
        if (Time.time - staminaRefreshTimer >= staminaRefreshLength)
        {

            if (Time.time - deflectTimer >= deflectLength)
            {
                deflecting = false;
                deflectTimer = float.PositiveInfinity;
                StaminaBar.fillAmount = 0;
                staminaRefreshTimer = Time.time;
                _animController.UpdateDeflect();
                currentFillCapacity = 0;
                deflects = 0;

            }
            else if ((SwipeInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.S)) && deflecting)
            {
                currentFillCapacity = StaminaBar.fillAmount;
                staminaRefreshTimer = Time.time;
                deflecting = false;
                deflects = 0;
                deflectTimer = float.PositiveInfinity;
                _animController.UpdateDeflect();
            }
            else if ((SwipeInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.W)) && !deflecting)
            {
                deflecting = true;
                deflectTimer = Time.time;
                _animController.UpdateDeflect();
            }
            else if (deflectTimer != float.PositiveInfinity)
            {
                StaminaBar.fillAmount = 1 - ((Time.time - deflectTimer) / deflectLength);
            }
        }
        else
        {
            StaminaBar.fillAmount = currentFillCapacity + ((Time.time - (staminaRefreshTimer)) / staminaRefreshLength);

            if (StaminaBar.fillAmount >= 1)
            {
                staminaRefreshTimer = 0;
            }
        }



    }
    //Plays the hit particle system. Activated by BulletScript
    public void PlayHitEffect(Transform t)
    {
        /*  float pitch = Random.Range(0.95f, 1.15f);
          if (Time.time - pitchComboTimer < deflectComboLength)
          {
              pitch = currentPitch + 0.02f;

          }
          currentPitch = pitch;
          pitchComboTimer = Time.time;*/

        float pitch = Random.Range(0.95f, 1.10f);
        string[] sounds = {"Deflect1","Deflect2","Deflect3","Deflect4"};
        float[] weightCDF = {0.45f,0.65f,0.85f,1};
        _am.PlaySoundFromArray(sounds, weightCDF,pitch);
       


        ParticleSystem particle = Instantiate(hitEffect, GetClosestObject(deflectParticlePositions, t).position, Quaternion.Euler(0, 100, 0), this.gameObject.transform);
        var main = particle.main;
        // main.simulationSpeed = 0.3f;
        particle.Play();

    }

    /* 
     * Resets the default pitch back to 1 after the combo time period has passed
     */
    void resetCurrentPitch()
    {
        if (Time.time - pitchComboTimer > deflectComboLength)
        {
            currentPitch = 1f;
            Debug.Log("reset");
        }
    }
    Transform GetClosestObject(List<Transform> objects, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in objects)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    /* 
     * Executed by bulletScript if within the range defined by the variable "bulletrange" in bulletScript's update function
     */
    public void BulletWithinRange()
    {
        if (deflecting)
        {
            deflects++;
            if (deflects >= 5)
            {
                deflects = 1;
            }
        }
    }
}
