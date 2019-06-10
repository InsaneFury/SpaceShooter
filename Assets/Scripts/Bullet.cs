using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeToDisable = 3f;

    private void OnEnable()
    {
        Invoke("DisableBullet", timeToDisable);
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
