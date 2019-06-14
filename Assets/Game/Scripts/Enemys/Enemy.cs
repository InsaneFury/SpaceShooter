using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Enemy : MonoBehaviour,IScoreable
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
    public float rateOfPowerUpsDrop = 0.10f;

    public int score{get;set;}

    public delegate void OnEnemyAction(IScoreable enemy);
    public event OnEnemyAction OnEnemyDie;

    CameraShakeInstance shaker;
    AudioManager aManager;

    Material mat;
    bool isBlinking = false;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        aManager = AudioManager.Get();
        score = (int)Random.Range(100f, 1000f);
        UIPopTextController.Initialize();
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

        switch (collision.tag)
        {
            case "nova":
            case "missile":
            case "ray":
                Die();
                break;
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

    void DieAction()
    {
        if (OnEnemyDie != null)
        {
            OnEnemyDie(this);
        }
    }

    void Die()
    {
        DieAction();
        Instantiate(explosion, gameObject.transform.position, explosion.transform.rotation);
        Instantiate(heatHazeWave, gameObject.transform.position, heatHazeWave.transform.rotation);
        DropLoot();
        aManager.Play("explosion");
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.10f, 2f);
        UIPopTextController.CreatePopText(score.ToString(), gameObject.transform);
        Destroy(gameObject);
    }

    void DropLoot()
    {
        int randDrop = (int)Random.Range(minStarsDrop, maxStarsDrop);
        for (int i = 0; i < randDrop; i++)
        {
            float randForceX = Random.Range(dispersionForce.x, dispersionForce.y);
            float randForceY = Random.Range(dispersionForce.x, dispersionForce.y);

            GameObject go = Instantiate(star, gameObject.transform.position, Quaternion.identity);

            go.GetComponent<Star>().OnPickUp += ScoreManager.Get().AddStars;
            Vector2 randForceDir = new Vector2(randForceX,randForceY);
            go.GetComponent<Rigidbody2D>().AddForce(randForceDir, ForceMode2D.Impulse);
        }

        float chanceToDropPW = Random.Range(0f, 1f);

        if (chanceToDropPW > rateOfPowerUpsDrop)
        {
            int pwToDrop = (int)Random.Range(0f, powerUps.Count);
            Instantiate(powerUps[pwToDrop], gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(powerUps[1], gameObject.transform.position, Quaternion.identity);
        }

    }
}
