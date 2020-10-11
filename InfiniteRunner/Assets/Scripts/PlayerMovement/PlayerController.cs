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




    int _playerPositionOffset = 0;
    public Vector3 getVelocity() { return _rb.velocity; }
    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _tf = this.GetComponent<Transform>();
        _anim = this.GetComponentInChildren<Animator>();
        _rb.velocity = new Vector3(speed, 0, 0);
    }
    
    private void Update()
    {
        Move();
        Slide();
        ChangeLookAtPoint();

        if (Input.GetKeyDown(KeyCode.F))
        {
            timeManager.SlowMotion();

        }
    }
    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _anim.SetTrigger("PlaySlideAnimation");
        }
     
    }
    void ChangeLookAtPoint()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            lookAtPoint.localPosition = new Vector3(0, 0.4f, 0);
        }
        else if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Sliding"))
        {
            lookAtPoint.localPosition = new Vector3(0, -0.4f, 0);
        }
    }
    void Move()
    {
        _rb.velocity = new Vector3(speed, 0, 0);

        if (Input.GetKeyDown(KeyCode.A) && _playerPositionOffset < 1)
        {
            _tf.position = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z + 1.5f);
            _playerPositionOffset += 1;
        }
        if (Input.GetKeyDown(KeyCode.D) && _playerPositionOffset > -1)
        {
            _tf.position = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z - 1.5f);
            _playerPositionOffset -= 1;
        }

    }

}
