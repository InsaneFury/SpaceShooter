using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonobehaviourSingleton<ScoreManager>
{
    [Header("ScoreSettings")]
    public int score = 0;
    public int highScore = 0;

    EnemySpawner spawner;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        spawner = EnemySpawner.Get();
        highScore = PlayerPrefs.GetInt("HighScore");
        //UIGameplayManager.Get().RefreshScoreUI();
    }

    void AddScoreFromEnemy(Enemy e)
    {
        if (score == spawner.scoreToBossSpawn)
        {
            spawner.SpawnFinalBoss();
        }
       // UIGameplayManager.Get().RefreshScoreUI();
    }

    public void AddHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
           // UIGameplayManager.Get().RefreshScoreUI();
        }   
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
       // UIGameplayManager.Get().RefreshScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
    }
}
