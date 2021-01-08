using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * Takes care of the game over screen when after the player runs out of health.
 * In charge of navigating the game over screen's ui and displaying unique
 * information regarding the previous game played.
 * 
 * @author Maxfield Barden
 */

public class GameOverState : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject inGameUI;
    public GameObject startMenu;
    public GameObject achievementTracker;
    public GameObject shop;
    private AchievementTracker achievements;
    private StartMenuScript sMenuScript;
    private int score;
    private int streak;
    private int bounty;

    // Start is called before the first frame update
    void Start()
    {
        inGameUI.GetComponent<ScoreCounter>().enabled = false;
        inGameUI.GetComponent<Canvas>().enabled = false;
        sMenuScript = startMenu.GetComponent<StartMenuScript>();
        achievements = achievementTracker.GetComponent<AchievementTracker>();

        DetermineHighScore(score);
        calculateBounty();
        DisplayScore();
        gameOverUI.GetComponent<Canvas>().enabled = true;

        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<PlayerAnimationController>().enabled = false;

    }

    /**
     * Displays the score from this current round.
     */
    private void DisplayScore()
    {
        if (IsHighScore())
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text
            = $"Final Score: {score}\n\nNew High Score!\n\nHighest Streak: {streak}\nPayout: ${bounty}";
        }
        else
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text
            = $"Final Score: {score}\nHighest Streak: {streak}\nPayout: ${bounty}";
        }
    }

    /**
     * Restarts the game upon being clicked.
     */
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    /**
     * Gets the score from the current round.
     * 
     * @param int The score from the round that was just completed.
     */
    public void GetScoreFromRound(int score)
    {
        this.score = score;
    }

    /**
     * Determines if the score from this round is a new high score.
     * 
     * @return True if this round's score is a new high score.
     */
    private bool IsHighScore()
    {
        return score > PlayerPrefs.GetInt("HighScore3", 0);
    }

    /**
     * Tells the achievements to update the high score chart if this is
     * a new high score.
     */
    private void DetermineHighScore(int score)
    {
        // if its higher than the lowest high score, it's a new high score.
        if (IsHighScore())
        {
            achievements.UpdateHighScores(score);
        }
    }

    /**
     * Gets the highest streak from the current round.
     * 
     * @param streak The highest streak from the current round.
     */
    public void GetHighestStreak(int streak) 
    {
        this.streak = streak;
    }
    /**
    * Calculates the money earned from the round
    */
    public void calculateBounty()
    {
        bounty = score / 100 + streak;
        shop.GetComponent<Shop>().depositMoney(bounty);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
