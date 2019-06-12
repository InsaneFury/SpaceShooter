using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float screenRatio;
    float orthographicWidth;

    private void Awake()
    {
        screenRatio = (float)Screen.width / (float)Screen.height;
        orthographicWidth = screenRatio * Camera.main.orthographicSize;
    }

    private void Update()
    {
        if (IsOutOfRange())
        {
            DisableBullet();
        }
    }

    private bool IsOutOfRange()
    {
        bool upLimit = transform.position.y > Camera.main.orthographicSize;
        bool downLimit = transform.position.y < -Camera.main.orthographicSize;
        bool rightLimit = transform.position.x > orthographicWidth;
        bool leftLimit = transform.position.x < -orthographicWidth;

        if (upLimit || downLimit || rightLimit || leftLimit)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DisableBullet();
        }
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
