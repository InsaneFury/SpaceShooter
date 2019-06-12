using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Life")]
    public float health = 20f;
    public GameObject explosion;
    public GameObject heatHazeWave;

    [Header("BlinkSettings")]
    [Tooltip("Time in seconds")]
    public float blinkTime = 0.2f;
    [Tooltip("Value 0f to 1f")]
    public float blinkAmount = 1f;

    [Header("ShakeSettings")]
    public float magnitud = 1f;
    public float roughness = 1f;
    public float fadeIn = 0.20f;
    public float fadeOut = 1;

    [Header("DropSettings")]
    public GameObject star;
    public List<GameObject> powerUps;
    public int minStarsDrop = 5;
    public int maxStarsDrop = 20;
    public Vector2 dispersionForce;

    CameraShakeInstance shaker;

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
            if (health <= 0)
            {
                Die();
            }
            TakeDamage();
            isBlinking = true;
            StartCoroutine("Blink");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Die();
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
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Instantiate(heatHazeWave, transform.position, heatHazeWave.transform.rotation);
        DropLoot();
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.10f, 2f);
        Destroy(gameObject);
    }

    void DropLoot()
    {
        int randDrop = (int)Random.Range(minStarsDrop, maxStarsDrop);
        for (int i = 0; i < randDrop; i++)
        {
            GameObject go = Instantiate(star, transform.position, Quaternion.identity);
            Vector2 randForceDir = new Vector2(Random.Range(dispersionForce.x, dispersionForce.y), Random.Range(dispersionForce.x, dispersionForce.y));
            go.GetComponent<Rigidbody2D>().AddForce(randForceDir, ForceMode2D.Impulse);
        }
    }
}
