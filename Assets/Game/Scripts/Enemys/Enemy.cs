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

    CameraShakeInstance shaker;
    AudioManager aManager;

    Material mat;
    bool isBlinking = false;

    public delegate void OnEnemyAction(IScoreable s);
    public static event OnEnemyAction OnEnemyDie;

    void Start()
    {
        OnEnemyDie += Die;
        mat = GetComponent<SpriteRenderer>().material;
        aManager = AudioManager.Get();
        score = (int)Random.Range(100f, 1000f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser") && !isBlinking)
        {
            if (health <= 0)
            {
                DieAction();
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
                DieAction();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            DieAction();
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

    void Die(IScoreable s)
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Instantiate(heatHazeWave, transform.position, heatHazeWave.transform.rotation);
        DropLoot();
        aManager.Play("explosion");
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.10f, 2f);
        OnEnemyDie -= Die;
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

        float chanceToDropPW = Random.Range(0f, 1f);

        if (chanceToDropPW > rateOfPowerUpsDrop)
        {
            int pwToDrop = (int)Random.Range(0f, powerUps.Count);
            Instantiate(powerUps[pwToDrop], transform.position, Quaternion.identity);
            Instantiate(powerUps[1], transform.position, Quaternion.identity);
        }

    }
}
