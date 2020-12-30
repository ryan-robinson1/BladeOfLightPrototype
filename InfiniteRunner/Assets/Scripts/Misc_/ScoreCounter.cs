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
    private int score;
    public TextMeshProUGUI scoreText;
    private int deflectScore = 5;
    private int attackScore = 50;

    /**
     * Called before the first frame update.
     */
    void Start()
    {
        score = 0;
        _animController = player.GetComponent<PlayerAnimationController>();
    }

    /**
     * Adds points to the total score when we deflect a bullet.
     */
    public void AddDeflectScore()
    {
        score += deflectScore;
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
