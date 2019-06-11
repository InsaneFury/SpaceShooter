using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameplayManager : MonobehaviourSingleton<UIGameplayManager>
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    Player player;
    ScoreManager sManager;
    GameManager gManager;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void RefreshScoreUI()
    {
        scoreText.text = sManager.score.ToString();
        highScoreText.text = sManager.highScore.ToString();
    }
}
