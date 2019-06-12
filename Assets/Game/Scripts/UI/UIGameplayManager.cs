using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameplayManager : MonobehaviourSingleton<UIGameplayManager>
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject energyBar;

    Player player;
    ScoreManager sManager;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        player = Player.Get();
        
    }

    void Update()
    {
        RefreshBar();
    }

    public void RefreshScoreUI()
    {
        scoreText.text = sManager.score.ToString();
        highScoreText.text = sManager.highScore.ToString();
    }

    void RefreshBar()
    {
        if (energyBar.transform.localScale.x != player.energy)
        {
            energyBar.transform.localScale = new Vector3(player.energy/100f, 1f, 1f);
        } 
    }
}
