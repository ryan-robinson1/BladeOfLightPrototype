using System.Collections;
using System.Text;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

/**
 * This script is in charge of navigating the settings UI.
 * 
 * @author Maxfield Barden
 */
public class SettingsNavigator : MonoBehaviour
{
    public GameObject menuMain;
    private Canvas[] achievementSubMenus;
    public GameObject displayPlane;
    public AudioManager _am;

    // Start is called before the first frame update
    void Start()
    {
        achievementSubMenus = this.GetComponentsInChildren<Canvas>();
    }

    /**
     * Enables the settings ui.
     */
    public void EnableSettingsMenu()
    {
        displayPlane.SetActive(true);
        menuMain.GetComponentInChildren<Canvas>().enabled = false;
        achievementSubMenus[0].enabled = true;
    }

    /**
     * Returns to the main menu upon the back button being clicked.
     */
    public void SettOnMainBackButtonClick()
    {
        menuMain.GetComponentInChildren<Canvas>().enabled = true;
        // main achievement menu
        achievementSubMenus[0].enabled = false;
        displayPlane.SetActive(false);
    }

    /**
     * Returns to main achievement menu upon back button getting clicked
     * while in how to play screen.
     */
    public void OnHowToPlayBackButton()
    {
        achievementSubMenus[1].enabled = false;
        achievementSubMenus[0].enabled = true;
    }

    /**
     * Displays the how to play information.
     */
    public void OnHowToPlay()
    {
        // disable main menu and enable how to play
        achievementSubMenus[0].enabled = false;
        achievementSubMenus[1].enabled = true;
    }

    /**
     * Handles the muting / unmuting functionality.
     */
    public void OnMuteButtonPress()
    {
        if (PlayerPrefs.GetString("muteMusic", "false") == "false")
        {
            _am.MuteAllSounds();
        }

        // otherwise, unmute
        else
        {
            _am.UnMuteAllSounds();
        }
    }
}
