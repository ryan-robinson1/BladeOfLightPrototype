using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    private int elims;
    private bool healed = false;

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
            = $"Final Score: {score}\nTotal Eliminations: {elims}\nHighest Streak: {streak}\nPayout: ${bounty}";
        }
    }

    /**
     * Restarts the game upon being clicked.
     */
    public void RestartGame()
    {
        PlayerPrefs.SetString("HeroColor", ColorDataBase.heroColorName);
        PlayerPrefs.SetString("EnemyColor", ColorDataBase.enemyColorName);
        PlayerPrefs.SetString("SwordColor", ColorDataBase.swordColorName);
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
        HandleScoringAchievements();
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
        HandleStreakAchievements();
    }

    /**
     * Gets the total number of elims from the current round.
     * 
     * @param elims The total number of eliminations from the current round.
     */
    public void GetElimTotal(int elims, bool healed)
    {
        this.elims = elims;
        this.healed = healed;
        HandleElimAchievements();
    }

    /**
    * Calculates the money earned from the round
    */
    public void calculateBounty()
    {
        // old payout system
        //  double g = (double)(((double)((double)score / 1000.0) + (double)streak)/100.0);
        //   double p = System.Math.Pow(g,(1.0/3.0))*175.0;

        // new payout system (every 1000 = 1 credit) rounds to next highest whole number
        // with a base of one per round
        double unRounded = score / 1000f;
        bounty = (int)Math.Ceiling(unRounded) + 1;
        Debug.Log(Math.Ceiling(unRounded));

        Debug.Log(unRounded);
        Debug.Log(Math.Ceiling(unRounded));

        shop.GetComponent<Shop>().depositMoney(bounty);
    }

    /**
     * Handles unlocking achievements based on the score from this round.
     */
    private void HandleScoringAchievements()
    {
        // will need to restructure this code to execute better
        if (PlayerPrefs.GetString("score1500", "locked") == "locked" && score >= 1500)
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("score1500");
        }
        if (PlayerPrefs.GetString("score15k", "locked") == "locked" && score >= 15000)
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("score15k");
        }
        if (PlayerPrefs.GetString("score50k", "locked") == "locked" && score >= 50000)
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("score50k");
        }

        //very rare to hit 200K so flip the if statement
        if (score >= 200000 && PlayerPrefs.GetString("score200k", "locked") == "locked")
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("score200k");
        }
    }

    /**
     * Handles unlocking achievements based on the highest streak from this round.
     */
    private void HandleStreakAchievements()
    {
        if (PlayerPrefs.GetString("streak25", "locked") == "locked" && streak >= 25)
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("streak25");
        }
    }

    /**
     * Handles unlocking achievements regarding eliminations.
     */
    private void HandleElimAchievements()
    {
        if (PlayerPrefs.GetString("kill200noheal", "locked") == "locked" && elims >= 200 && !healed)
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("kill200noheal");
        }

        if (elims == 0 && score >= 5000 && PlayerPrefs.GetString("passive5k", "locked") == "locked")
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("passive5k");
        }

        if (elims == 0 && score >= 20000 && PlayerPrefs.GetString("passive20k", "locked") == "locked")
        {
            AchievementEvents.aEvents.UnlockAchievementTrigger("passive20k");
        }
    }
}
