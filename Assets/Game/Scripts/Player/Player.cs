using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonobehaviourSingleton<Player>
{
    [Header("PlayerSettings")]
    public float speed = 10f;
    public float maxVelocity = 100f;
    public float energy = 100f;
    public float energyLoseRateInSeconds = 1f;
    public float energyLoseAmount = 0.01f;
    public float takeDMG = 2f;

    [Header("PowerUpSettings")]
    public GameObject laser;
    public GameObject doubleLaser;
    public GameObject epicLaser;
    public GameObject epicLaserText;
    public GameObject shield;
    public int energyPW = 10;

    [Header("SkillsSettings")]
    public GameObject novaText;
    public GameObject nova;
    public float waitTimeToNova = 10f;
    float timer = 0f;

    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 moveVelocity;
    Vector2 playerSize;
    Vector2 screenBounds;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        playerSize.x = GetComponent<SpriteRenderer>().bounds.extents.x;
        playerSize.y = GetComponent<SpriteRenderer>().bounds.extents.y;
        moveInput = Vector2.zero;
        moveVelocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        StartEnergyDrain();
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;

        if (energy <= 0f)
        {
            energy = 0f;
            StopEnergyDrain();
        }  
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.AddForce(moveVelocity * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {
        ScreenPlayerLimit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BadLaser"))
        {
            if (energy > 0f)
            {
                TakeDamage();
            }
        }

        switch (collision.tag)
        {
            case "shield":
                ActiveShield();
                Destroy(collision.gameObject);
                break;
            case "epic_laser":
                ActiveEpicLaser();
                Destroy(collision.gameObject);
                break;
            case "double_laser":
                ActiveDoubleLaser();
                Destroy(collision.gameObject);
                break;
            case "energy":
                AddEnergy();
                Destroy(collision.gameObject);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (energy > 0f)
            {
                TakeDamage();
            }
        }
    }

    void ScreenPlayerLimit()
    {
        Vector2 pos = transform.position;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float orthographicWidth = screenRatio * Camera.main.orthographicSize;

        bool upLimit = pos.y + playerSize.y > Camera.main.orthographicSize;
        bool downLimit = pos.y - playerSize.y < -Camera.main.orthographicSize;
        bool rightLimit = pos.x + playerSize.x > orthographicWidth;
        bool leftLimit = pos.x - playerSize.x < -orthographicWidth;


        if (upLimit)
        {
            pos.y = Camera.main.orthographicSize - playerSize.y;
        }
        if (downLimit)
        {
            pos.y = -Camera.main.orthographicSize + playerSize.y;
        }

        if (rightLimit)
        {
            pos.x = orthographicWidth - playerSize.x;
        }
        if (leftLimit)
        {
            pos.x = -orthographicWidth + playerSize.x;
        }

        transform.position = pos;
    }

    void LoseEnergy()
    {
        energy -= energyLoseAmount;
    }

    public void StartEnergyDrain()
    {
        InvokeRepeating("LoseEnergy", 0f, energyLoseRateInSeconds);
    }

    public void StopEnergyDrain()
    {
        CancelInvoke("LoseEnergy");
    }

    public void TakeDamage()
    {
        energy -= takeDMG;
    }

    public void ActiveShield()
    {
        shield.SetActive(true);
    }

    public void ActiveEpicLaser()
    {
        laser.SetActive(false);
        doubleLaser.SetActive(false);
        epicLaser.SetActive(true);
        
        epicLaserText.SetActive(true);
    }

    public void ActiveDoubleLaser()
    {
        laser.SetActive(false);
        doubleLaser.SetActive(true);
        epicLaser.SetActive(false);
    }

    public void ActiveSimpleLaser()
    {
        laser.SetActive(true);
        doubleLaser.SetActive(false);
        epicLaser.SetActive(false);
    }

    public void AddEnergy()
    {
        energy += energyPW;
    }

    public void Nova()
    {
        if (timer > waitTimeToNova)
        {
            nova.SetActive(true);
            timer = 0f;
        }
        else
        {
            novaText.SetActive(true);
        }
    }

    public void Missile()
    {
        Debug.Log("MissileLaunch");
    }

    public void Ray()
    {
        Debug.Log("Ray");
    }
}
