using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    GameManager gManager;
    ScoreManager sManager;

    void Start()
    {
        sManager = ScoreManager.Get();
        gManager = GameManager.Get();
    }

    public void RetryBTN()
    {
        gManager.Retry();
        sManager.ResetScore();
        sManager.ResetStars();
        sManager.ResetEnemys();
    }
}
