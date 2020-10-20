using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour
{
    public CameraFollowScript camera;

    private float slowdownFactor = 0.1f;
    private float slowMotionLength = 0.15f;
    private float reentraceSpeed = 1.3f;



    float _slowMotionTimer = float.PositiveInfinity;


    private void Update()
    {
        ResetSlowMotionTimer();
    }
    private void DoSlowMotion()
    {

        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
        _slowMotionTimer = Time.time;
        //camera.followSpeed = 40f;

    }
    private void UndoSlowMotion()
    {
        Time.timeScale += reentraceSpeed * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime = Time.deltaTime / 2f;
       
        if (Time.timeScale == 1)
        {
            _slowMotionTimer = float.PositiveInfinity;
            Time.fixedDeltaTime = 0.01f;
            //camera.followSpeed = 10f;
        }
    }
    private void ResetSlowMotionTimer()
    {
        if (Time.time - _slowMotionTimer > slowMotionLength)
        {
            UndoSlowMotion();
        }
    }
    public void SlowMotion()
    {
        DoSlowMotion();
    }
    public void SlowMotion(float _slowDownFactor,float _slowMotionLength,float _reentranceSpeed)
    {
        slowdownFactor = _slowDownFactor;
        slowMotionLength = _slowMotionLength;
        reentraceSpeed = _reentranceSpeed;
        DoSlowMotion();
    }
  
    
}
