using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Life")]
    public float health = 20f;
    public GameObject explosion;

    [Header("BlinkSettings")]
    [Tooltip("Time in seconds")]
    public float blinkTime = 0.2f;
    [Tooltip("Value 0f to 1f")]
    public float blinkAmount =1f;

    Material mat;
    bool isBlinking = false;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser") && !isBlinking)
        {
            Die();
            TakeDamage();
            isBlinking = true;
            StartCoroutine("Blink");
        }
    }

    IEnumerator Blink()
    {
        mat.SetFloat("_FlashAmount", blinkAmount);
        yield return new WaitForSeconds(blinkTime);
        mat.SetFloat("_FlashAmount", 0f);
        isBlinking = false;
    }

    void TakeDamage()
    {
        health--;
    }

    void Die()
    {
        if (health <= 0)
        {
            Instantiate(explosion, transform.position,explosion.transform.rotation);
            Destroy(gameObject);
        }  
    }
}
