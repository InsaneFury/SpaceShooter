using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonobehaviourSingleton<GameManager>
{
    [Header("Settings")]
    Player player;
    ScoreManager sManager;
    ScenesManager sceneM;
    public bool gameOver = false;
    public bool victory = false;
    public bool isInitialized = false;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        Init();
        isInitialized = false;
    }

    void Update()
    {
        if (player.energy <= 0 && !gameOver)
        {
            GameOver();
            isInitialized = false;
        }
        else if ((player.energy > 0 && sManager.score > 10000))
        {
            Victory();
            isInitialized = false;
        }
        if ((sceneM.GetActualScene() == "Level_01") && !isInitialized)
        {
            Init();
        }
    }

    void GameOver()
    {
        victory = false;
        gameOver = true;
        sceneM.LoadScene("GameOver");
    }

    void Victory()
    {
        victory = true;
        sManager.AddHighScore();
        gameOver = true;
        sceneM.LoadScene("GameOver");
    }

    public void Retry()
    {
        gameOver = false;
    }

    void Init()
    {
        sceneM = ScenesManager.Get();
        sManager = ScoreManager.Get();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        gameOver = false;
        player.energy = 100f;
        Debug.Log(player.energy);
        isInitialized = true;
    }
}
