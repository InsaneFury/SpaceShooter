using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float maxVelocity = 100f;

    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 moveVelocity;
    Vector2 playerSize;
    Vector2 screenBounds;

    void Start()
    {
        playerSize.x = GetComponent<SpriteRenderer>().bounds.extents.x;
        playerSize.y = GetComponent<SpriteRenderer>().bounds.extents.y;
        moveInput = Vector2.zero;
        moveVelocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;    
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
}
