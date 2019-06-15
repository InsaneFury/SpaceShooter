using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI stars;

    ScoreManager sManager;

    void Start()
    {
        sManager = ScoreManager.Get();
        RefreshScoreUI();
    }

    public void RefreshScoreUI()
    {
        if (sManager)
        {
            scoreText.text = sManager.score.ToString();
            highScoreText.text = sManager.highScore.ToString();
            stars.text = sManager.stars.ToString();
        }
    }
}
