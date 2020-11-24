using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    PlayerAnimationController _animController;

    private void Start()
    {
        //StartGame();
    }
    /**
     *  Resets the camera and player position the game configuration
     */
    public void StartGame()
    {
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        camera.transform.rotation = Quaternion.Euler(31, 90, 0);
        _animController = player.GetComponent<PlayerAnimationController>();

        player.GetComponent<PlayerController>().enabled = true;
        _animController.StartGame();
        camera.GetComponent<CameraFollowScript>().enabled = true;
        this.GetComponentInChildren<Canvas>().enabled = false;
    }
}
