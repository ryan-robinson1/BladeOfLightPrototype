using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        inGameUI.GetComponent<ScoreCounter>().enabled = false;
        inGameUI.GetComponent<Canvas>().enabled = false;

        DisplayScore();
        gameOverUI.GetComponent<Canvas>().enabled = true;

        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<PlayerAnimationController>().enabled = false;

        Debug.Log(score);
    }

    /**
     * Displays the score from this current round.
     */
    private void DisplayScore()
    {
        gameOverUI.GetComponentInChildren<Text>().text
            = "Final Score: " + score;
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
