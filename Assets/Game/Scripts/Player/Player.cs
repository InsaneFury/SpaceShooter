using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonobehaviourSingleton<Player>
{
    public float speed = 10f;
    public float maxVelocity = 100f;
    public float energy = 100f;
    public float energyLoseRateInSeconds = 1f;
    public float energyLoseAmount = 0.01f;
    public float takeDMG = 2f;

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
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
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
        InvokeRepeating("LoseEnergy",0f, energyLoseRateInSeconds);
    }

    public void StopEnergyDrain()
    {
        CancelInvoke("LoseEnergy");
    }

    public void TakeDamage()
    {
        energy -= takeDMG;
    }
}
