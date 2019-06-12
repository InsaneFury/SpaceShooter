using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [Header("StarSettings")]
    public Transform target;
    public float minSpeed;
    public float maxSpeed;
    public float timeToBePicked = 0.4f;
    float screenRatio;
    float orthographicWidth;

    float speed;
    bool isFollowing = false;
    bool canPickUp = false;

    Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        screenRatio = (float)Screen.width / (float)Screen.height;
        orthographicWidth = screenRatio * Camera.main.orthographicSize;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("StarCollector").transform;
        Invoke("NoReadyToPickUp", timeToBePicked-1);
        Invoke("ReadyToPickUp", timeToBePicked); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canPickUp)
        {
            isFollowing = true;
        }
        if (collision.CompareTag("StarCollector"))
        {
            Disable();
        }
    }

    private void Update()
    {
        if (isFollowing)
        {
            Move();
        }
        if (IsOutOfRange())
        {
            Disable();
        }
    }

    public void Move()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime);
    }

    public void Disable()
    {
        Destroy(gameObject);
    }

    void NoReadyToPickUp()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }
    void ReadyToPickUp()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        canPickUp = true;
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
}
