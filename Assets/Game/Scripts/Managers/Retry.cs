using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    GameManager gManager;

    void Start()
    {
        gManager = GameManager.Get();
    }

    public void RetryBTN()
    {
        gManager.Retry();
    }
}
