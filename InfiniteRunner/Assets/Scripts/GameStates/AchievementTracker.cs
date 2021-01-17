using System.Collections;
using System.Text;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

/**
 * This script is in charge of navigating the achievments menu UI
 *  and handling the achievement unlock for the shop.
 * 
 * @author Maxfield Barden
 */
public class AchievementTracker : MonoBehaviour
{
    public GameObject mainMenu;
    private Canvas[] achievementSubMenus;
    private int[] highScores;
    private AchievementHandler _acHandler;

    // Start is called before the first frame update
    void Start()
    {
        highScores = GetHighScores();
        achievementSubMenus = this.GetComponentsInChildren<Canvas>();
        _acHandler = this.GetComponentInParent<AchievementHandler>();
    }

    /**
     * Returns to the main menu upon the back button being clicked.
     */
    public void OnMainBackButtonClick()
    {
        mainMenu.GetComponentInChildren<Canvas>().enabled = true;
        // main achievement menu
        achievementSubMenus[0].enabled = false;
    }

    /**
     * Returns to main achievement menu upon back button getting clicked
     * while in high score screen.
     */
    public void OnHighScoreBackButton()
    {
        achievementSubMenus[1].enabled = false;
        achievementSubMenus[0].enabled = true;
    }

    /**
     * Displays the high scores upon the high scores button being clicked.
     */
    public void OnHighScoreClick()
    {
        // disable main menu and enable high score menu
        achievementSubMenus[0].enabled = false;
        achievementSubMenus[1].enabled = true;
        achievementSubMenus[1].GetComponentInChildren<Text>().text = DisplayHighScores();
    }

    /**
     * Logic for writing out the high scores in text format so they
     * can be displayed on the screen.
     */
    private string DisplayHighScores()
    {
        string highScoreDisplay =
            $"High Scores:\n\n1. {highScores[0]}\n\n2. {highScores[1]}\n\n3. {highScores[2]}";
        return highScoreDisplay;
    }

    /**
     * Sets a new high score upon the player earning one during the previous match.
     */
    public void UpdateHighScores(int newScore) 
    { 
        // if its greater than the highest score
        if (newScore > highScores[0])
        {
            PlayerPrefs.SetInt("HighScore1", newScore);
            PlayerPrefs.SetInt("HighScore2", highScores[0]);
            PlayerPrefs.SetInt("HighScore3", highScores[1]);
        }
        else if (newScore > highScores[1])
        {
            PlayerPrefs.SetInt("HighScore2", newScore);
            PlayerPrefs.SetInt("HighScore3", highScores[1]);
        }
        else 
        {
            PlayerPrefs.SetInt("HighScore3", newScore);
        }
        // update the local array at runtime
        highScores = GetHighScores();
    }

    /**
     * Gets the player's highscores upon loading the game.
     * 
     * @return the list of high scores.
     */
    private int[] GetHighScores()
    {
        int[] highScores = new int[3];
        highScores[0] = PlayerPrefs.GetInt("HighScore1", 0);
        highScores[1] = PlayerPrefs.GetInt("HighScore2", 0);
        highScores[2] = PlayerPrefs.GetInt("HighScore3", 0);
        return highScores;
    }

    /**
     * Called when we click on the Achievements button on the main screen.
     */
    public void OnAchievementButton()
    {
        achievementSubMenus[0].enabled = false;
        achievementSubMenus[2].enabled = true;
        achievementSubMenus[2].GetComponentInChildren<TextMeshProUGUI>().text = DisplayAchievementInfo();
    }

    /**
     * Called when the back button on the achievement menu is clicked.
     */
    public void OnAchievementBackButton()
    {
        achievementSubMenus[2].enabled = false;
        achievementSubMenus[0].enabled = true;
    }

    /**
     * Displays the achievement information on the achievement menu page.
     */
    private string DisplayAchievementInfo()
    {
        Dictionary<string, Achievement> a = _acHandler.GetAchievements();
        StringBuilder str = new StringBuilder();
        foreach (var item in a)
        {
            str.Append($"{item.Value.GetDisplayName()} is {item.Value.GetLockState()}\n{item.Value.GetDescription()}\n\n");
        }

        return str.ToString();
    }
}
