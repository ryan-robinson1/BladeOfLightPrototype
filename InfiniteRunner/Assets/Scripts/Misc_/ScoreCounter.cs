using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;

    /**
     * Called before the first frame update.
     */
    void Start()
    {
        int offsetX = 100;
        int offsetY = 100;
        score = 0;
        scoreText.rectTransform.position = new Vector2(Screen.width - offsetX, Screen.height - offsetY);
    }

    /**
     * Update is called once per frame.
     */
    void Update()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }
}
