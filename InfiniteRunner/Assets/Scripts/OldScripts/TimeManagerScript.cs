using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour
{
    public CameraFollowScript camera;

    private float _slowdownFactor = 0.1f;
    private float _slowMotionLength = 0.15f;
    private float _reentraceSpeed = 1.3f;
    private bool paused = false;
    public GameObject audioHolder;
    private AudioManager audioManager;


    float _slowMotionTimer = float.PositiveInfinity;


    /**
     * Called before the first frame.
     */
    private void Start()
    {
        audioManager = audioHolder.GetComponent<AudioManager>();
    }

    private void Update()
    {
       
    }
    
    /**
     * Pauses the game.
     */
    public void PauseGame()
    {
        if (PlayerPrefs.GetString("paused", "false") == "false")
        {
            Time.timeScale = 0;
            PlayerPrefs.SetString("paused", "true");
        }
        else
        {
            Time.timeScale = 1;
            PlayerPrefs.SetString("paused", "false");
        }
    }
    private void DoSlowMotion()
    {

        Time.timeScale = _slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
        _slowMotionTimer = Time.time;
        //camera.followSpeed = 40f;

    }

    private void UndoSlowMotion()
    {
        Time.timeScale += _reentraceSpeed * Time.unscaledDeltaTime;
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
        if (Time.time - _slowMotionTimer > _slowMotionLength)
        {
            UndoSlowMotion();
        }
    }
    public void SlowMotion()
    {
        DoSlowMotion();
    }
    public void SlowMotion(float slowDownFactor,float slowMotionLength,float reentranceSpeed)
    {
        _slowdownFactor = slowDownFactor;
        _slowMotionLength = slowMotionLength;
        _reentraceSpeed = reentranceSpeed;
        DoSlowMotion();
    }
  
    
}
