using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public GameObject UI;
    public Canvas shopMenu;
   [HideInInspector]
    public GameObject achievements;
    PlayerAnimationController _animController;
    public bool started = false;
    private Quaternion targetCameraRotation;
    public GameObject chunkGenerator;
    int renderDistance = 3;
    //public List<Material> uiButtonMats;
    private void Start()
    {
        //StartGame();
        //foreach (Material material in uiButtonMats)
        // {
        // material.SetColor("buttonColor", ColorDataBase.GetUIColor());
        // }
        targetCameraRotation = Quaternion.Euler(23f, 90f, 0f);
        shopMenu.enabled = false;
    }

    /**
     * Updates the game each frame.
     */
    private void FixedUpdate()
    {
        // keeps the player from inching off screen
        if (!started)
        {
               player.transform.position = new Vector3(
            -2f, 0.745f, 0);

        }
        else
        {
            // rotates the camera out
            camera.transform.rotation = Quaternion.Lerp(
                camera.transform.rotation, targetCameraRotation, 3.5f * Time.deltaTime);
        }

        DisableScript();
    }

    /**
     * Disables the script once the camera is rotated fully so the update
     * function stops calling and saves memory.
     */
    private void DisableScript()
    {
        if (camera.transform.rotation == targetCameraRotation)
        {
            this.enabled = false;
        }
    }

    /**
     * Transfers over to the achievements menu. Called by onClick()
     * function of the Trophy button at the main menu.
     */
    public void OnTrophyClick()
    {
        achievements.GetComponentInChildren<Canvas>().enabled = true;
        this.GetComponentInChildren<Canvas>().enabled = false;
    }

    /**
     *  Resets the camera and player position the game configuration
     */
    public void StartGame()
    {
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        _animController = player.GetComponent<PlayerAnimationController>();
        UI.GetComponent<Canvas>().enabled = true;
        UI.GetComponent<ScoreCounter>().enabled = true;


        player.GetComponent<Rigidbody>().transform.position = new Vector3(0, 1.15f, 0);
        player.GetComponent<PlayerController>().enabled = true;
        _animController.StartGame();
        camera.GetComponent<CameraFollowScript>().enabled = true;
        this.GetComponentInChildren<Canvas>().enabled = false;
        started = true;
        for(int i =0;i<renderDistance;i++)
        {
            chunkGenerator.GetComponent<ChunkGenerator>().generateChunk(new Vector3(90, 0, 0), Quaternion.Euler(0, 90, 0));
        }
        
        //this.enabled = false;
        
    }
   
}
