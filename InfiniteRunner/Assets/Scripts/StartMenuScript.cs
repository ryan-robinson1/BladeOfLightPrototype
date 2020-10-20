using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    private void Start()
    {
        //StartGame();
    }
    public void StartGame()
    {
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        camera.transform.rotation = Quaternion.Euler(31, 90, 0);

        player.GetComponent<PlayerController>().enabled = true;
        camera.GetComponent<CameraFollowScript>().enabled = true;
        this.GetComponentInChildren<Canvas>().enabled = false;
    }
}
