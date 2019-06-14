using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonobehaviourSingleton<ScoreManager>
{
    [Header("ScoreSettings")]
    public int score = 0;
    public int highScore = 0;
    public int stars = 0;

    EnemySpawner spawner;
    UIGameplayManager UIManager;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        spawner = EnemySpawner.Get();
        highScore = PlayerPrefs.GetInt("HighScore");
        UIManager = UIGameplayManager.Get();
        UIManager.RefreshScoreUI();
    }

    public void AddScore(IScoreable e)
    {
        score += e.score;

        if (score > highScore)
            AddHighScore();

        if (score == spawner.scoreToBossSpawn)
        {
            spawner.SpawnFinalBoss();
        }
        UIManager.RefreshScoreUI();
    }

    public void AddHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UIManager.RefreshScoreUI();
        }   
    }

    public void AddStars(IScoreable star)
    {
        stars += star.score;
        UIManager.RefreshScoreUI();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        UIManager.RefreshScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
    }
}
