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
    public int level01Score = 10000;
    public int level02Score = 20000;

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
        bool isLevel01 = (sceneM.GetActualScene() == "Level_01");
        bool isLevel02 = (sceneM.GetActualScene() == "Level_02");

        if (isLevel01 || isLevel02)
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            if (player.energy <= 0 && !gameOver)
            {
                GameOver();
                isInitialized = false;
            }
            else if ((player.energy > 0 && (sManager.score > level01Score)) && isLevel01)
            {
                sceneM.LoadScene("Level_02");
                isInitialized = true;
            }
            else if ((player.energy > 0 && (sManager.score > level02Score)))
            {
                Victory();
                isInitialized = false;
            }
        }

        if (((isLevel01 || isLevel02) && !isInitialized))
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
        player.energy = 100f;
        gameOver = false;

        isInitialized = true;
    }
}
