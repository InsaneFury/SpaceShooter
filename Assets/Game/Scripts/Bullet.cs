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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DisableBullet();
        }
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
