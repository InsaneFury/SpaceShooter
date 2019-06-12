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
    public GameObject blurBG;
    public GameObject blurMenu;

    Player player;
    ScoreManager sManager;

    public bool isBulletTimeOn = false;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        sManager = ScoreManager.Get();
        player = Player.Get();
    }

    void Update()
    {
        RefreshBar();
        BulletTimeMenu();
    }

    //public void RefreshScoreUI()
    //{
    //    scoreText.text = sManager.score.ToString();
    //    highScoreText.text = sManager.highScore.ToString();
    //}


    void RefreshBar()
    {
        if (energyBar.transform.localScale.x != player.energy)
        {
            energyBar.transform.localScale = new Vector3(player.energy / 100f, 1f, 1f);
        }
    }

    public void BulletTimeMenu()
    {
        if (Input.GetKeyDown("e") && !isBulletTimeOn)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            blurBG.SetActive(true);
            blurMenu.SetActive(true);
            isBulletTimeOn = true;
        }
        else if (Input.GetKeyDown("e"))
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            isBulletTimeOn = false;
            blurBG.SetActive(false);
            blurMenu.SetActive(false);
        }
    }

    public void disableBulletTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        isBulletTimeOn = false;
        blurBG.SetActive(false);
        blurMenu.SetActive(false);
    }
}
