using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public GameObject UI;
    PlayerAnimationController _animController;
    public bool started = false;

    //public List<Material> uiButtonMats;
    private void Start()
    {
        //StartGame();
        //foreach (Material material in uiButtonMats)
       // {
           // material.SetColor("buttonColor", ColorDataBase.GetUIColor());
       // }
    }

    /**
     * Updates the game each frame.
     */
    private void FixedUpdate()
    {
        // keeps the player from inching off screen
        player.transform.position = new Vector3(
            0f, player.transform.position.y, player.transform.position.z);
    }
    /**
     *  Resets the camera and player position the game configuration
     */
    public void StartGame()
    {
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        camera.transform.rotation = Quaternion.Euler(28f, 90, 0);
        _animController = player.GetComponent<PlayerAnimationController>();
        UI.GetComponent<Canvas>().enabled = true;

        player.GetComponent<Rigidbody>().transform.position = new Vector3(0, 1.15f, 0);
        player.GetComponent<PlayerController>().enabled = true;
        _animController.StartGame();
        camera.GetComponent<CameraFollowScript>().enabled = true;
        this.GetComponentInChildren<Canvas>().enabled = false;
        started = true;
        this.enabled = false;
    }
   
}
