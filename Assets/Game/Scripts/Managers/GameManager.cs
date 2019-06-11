using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonobehaviourSingleton<GameManager>
{
    [Header("Settings")]
    Player player;
    ScoreManager sManager;
    public bool gameOver = false;
    public bool victory = false;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        /*if (player.lives <= 0 && player.canMove)
        {
            GameOver();
        }
        else if ((player.lives > 0 && player.canMove))
        {
            Victory();
        }*/
    }

    void GameOver()
    {
        victory = false;
        gameOver = true;
        ScenesManager.Get().LoadScene("GameOver");
    }

    void Victory()
    {
        victory = true;
        sManager.AddHighScore();
        gameOver = true;
        ScenesManager.Get().LoadScene("GameOver");
    }

    public void Retry()
    {
        gameOver = false;
        sManager.ResetScore();
    }

    void Init()
    {
        sManager = ScoreManager.Get();
    }
}
