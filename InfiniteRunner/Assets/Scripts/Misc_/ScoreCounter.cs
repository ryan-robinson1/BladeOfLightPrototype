using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * Handles the scoring system in the game. Gives players points for deflecting,
 * attacking, as well as distance ran. Will store as a high score if it is 
 * higher than the lowest high score.
 * 
 * @author Maxfield Barden
 */

public class ScoreCounter : MonoBehaviour
{
    public GameObject player;
    private PlayerAnimationController _animController;
    private GameOverState gmOverState;
    private int score;
    public TextMeshProUGUI scoreText;
    private int deflectScore = 5;
    private int attackScore = 50;
    private int highestStreak;

    /**
     * Called before the first frame update.
     */
    void Start()
    {
        score = 0;
        highestStreak = 0;
        _animController = player.GetComponent<PlayerAnimationController>();
        gmOverState = player.GetComponent<GameOverState>();
    }

    /**
     * Sends the game over screen the score from this round when the player dies.
     */
    private void OnDisable()
    {
        _animController.resetAttackMultiplier();
        this.SendRoundInfo();
    }

    /**
     * Sends the game over state the information from the current round.
     */
    private void SendRoundInfo()
    {
        gmOverState.GetScoreFromRound(score);
        gmOverState.GetHighestStreak(highestStreak);
    }

    /**
     * Adds points to the total score when we deflect a bullet.
     */
    public void AddDeflectScore()
    {
        score += deflectScore;
    }

    /**
     * Compares the current streak that just broke to the current
     * highest streak on record. If the streak being passed in
     * is greater than the current highest streak, it becomes
     * the new highest streak. Only used for calculating high
     * streaks during the current round.
     * 
     * @param streak The streak we are comparing the current
     * highest streak to.
     */
    public void SetHighestStreak(int streak)
    {
        if (streak > highestStreak)
        {
            highestStreak = streak;
        }
    }

    /**
     * Adds points to the total score when we kill an enemy.
     * Every 10 kill streaks the multiplier for the score increases.
     */
    public void AddAttackScore(int streakMultiplier)
    {
        score += (attackScore * streakMultiplier);
    }

    /**
     * Update is called once per frame.
     */
    void Update()
    {
        score++;
        scoreText.text = 
            $"Score: {score} \nStreak: {_animController.GetStreak()}\nMultiplier: {_animController.GetStreakMultiplier()}";
    }
}
