using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public TimeManagerScript timeManager;



    [SerializeField]
    private Transform lookAtPoint;


    private float speed = 15f;
    Rigidbody _rb;
    Transform _tf;
    Animator _anim;
    BoxCollider _bc;

   



    int _playerPositionOffset = 0;
 
    private void Start()
    {
        
        _rb = this.GetComponent<Rigidbody>();
        _tf = this.GetComponent<Transform>();
        _anim = this.GetComponentInChildren<Animator>();
        _bc = this.GetComponent<BoxCollider>();
        _rb.velocity = new Vector3(speed, 0, 0);
       
    }
    public Vector3 getVelocity() { return new Vector3(speed,0,0); }

    private void Update()
    {
        Move();
        Slide();
        ChangeLookAtPoint();

        if (Input.GetKeyDown(KeyCode.Space) || SwipeInput.Instance.DoubleTap)
        {
            timeManager.SlowMotion();

        }
    }
    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.S) || SwipeInput.Instance.SwipeDown)
        {
            _anim.SetTrigger("PlaySlideAnimation");
        }
     
    }
    void ChangeLookAtPoint()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            lookAtPoint.localPosition = new Vector3(0, 0.4f, 0);
            _bc.size = new Vector3(_bc.size.x, 1.731701f, _bc.size.z);
            _bc.center = new Vector3(_bc.center.x, 0, _bc.center.z);
        }
        else if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Sliding"))
        {
            lookAtPoint.localPosition = new Vector3(0, -0.4f, 0);
            _bc.size = new Vector3(_bc.size.x, 0.4f, _bc.size.z);
            _bc.center = new Vector3(_bc.center.x, -0.4f, _bc.center.z);
        }
    }
    void Move()
    {
        _rb.velocity = new Vector3(speed, 0, 0);

        if ((Input.GetKeyDown(KeyCode.A) || SwipeInput.Instance.SwipeLeft)&& _playerPositionOffset < 1)
        {
            _tf.position = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z + 2f);
            _playerPositionOffset += 1;
        }
        if ((Input.GetKeyDown(KeyCode.D) || SwipeInput.Instance.SwipeRight) && _playerPositionOffset > -1)
        {
            _tf.position = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z - 2f);
            _playerPositionOffset -= 1;
        }

    }
   

}
