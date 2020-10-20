using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SwipeInput : MonoBehaviour
{
    #region Instance
    private static SwipeInput instance;
    public static SwipeInput Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SwipeInput>();
                if(instance == null)
                {
                    instance = new GameObject("Spawned SwipeInput", typeof(SwipeInput)).GetComponent<SwipeInput>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    [Header("Tweaks")]
    [SerializeField] private float deadzone = 100.0f;
    [SerializeField] private float doubleTapDelta = 0.5f;

    [Header("Logic")]
    private bool tap, doubleTap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;
    private float lastTap;
    private float sqrDeadzone;

    #region Public properties
    public bool Tap
    {
        get { return tap; }
    }
    public bool DoubleTap
    {
        get { return doubleTap; }
    }
    public Vector2 SwipeDelta
    {
        get { return swipeDelta; }
    }
    public bool SwipeLeft
    {
        get { return swipeLeft; }
    }
    public bool SwipeRight
    {
        get { return swipeRight; }
    }
    public bool SwipeUp
    {
        get { return swipeUp; }
    }
    public bool SwipeDown
    {
        get { return swipeDown; }
    }




    #endregion

    private void Start()
    {
        sqrDeadzone = deadzone * deadzone;
    }
    private void Update()
    {
        tap = doubleTap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
#if UNITY_EDITOR
        UpdateStandalone();
#else
        UpdateMobile();
#endif
    }

    private void UpdateStandalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
            doubleTap = Time.time - lastTap < doubleTapDelta;
            lastTap = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero && Input.GetMouseButton(0))
            swipeDelta = (Vector2)Input.mousePosition - startTouch;

        if(swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }
    private void UpdateMobile()
    {
        if (Input.touches.Length !=0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
                doubleTap = Time.time - lastTap < doubleTapDelta;
                lastTap = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }

        }
        

        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero && Input.touches.Length != 0)
            swipeDelta = Input.touches[0].position - startTouch;

        if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }
}


