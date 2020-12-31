using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is in charge of navigating the achievments menu UI.
 * 
 * @author Maxfield Barden
 */
public class AchievementTracker : MonoBehaviour
{
    public GameObject mainMenu;
    private Canvas achievementsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        achievementsCanvas = this.GetComponentInChildren<Canvas>();
    }

    /**
     * Returns to the main menu upon the back button being clicked.
     */
    public void OnBackButtonClick()
    {
        mainMenu.GetComponentInChildren<Canvas>().enabled = true;
        achievementsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
