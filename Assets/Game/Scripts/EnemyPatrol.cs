using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Settings")]
    public float speed;
    public float startWaitTime;
    public Transform point;
    public Transform enemy;
    public float stopFollowDistance;
    public bool squadMode = false;

    float waitTime;
    float minDistanceToPoint = 0.2f;
    float screenRatio;
    float orthographicWidth;
    float enemySpeedRotation = 8f;
    float maxYPatrol = 3f;
    Camera cam;
    Transform player;
    Vector2 enemySize;

    void Start()
    {
        enemySize.x = GetComponent<SpriteRenderer>().bounds.extents.x;
        enemySize.y = GetComponent<SpriteRenderer>().bounds.extents.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        waitTime = startWaitTime;
        cam = Camera.main;
        screenRatio = (float)Screen.width / (float)Screen.height;
        orthographicWidth = screenRatio * cam.orthographicSize;
    }

    void Update()
    {
        LookToPlayer();
        if (squadMode)
        {
            FollowEnemy();
        }
        else
        {
            MoveToRandomWaypoint(); 
        } 
    }

    void MoveToRandomWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, point.position) < minDistanceToPoint)
        {
            Vector2 RandomPoint = new Vector2(Random.Range(-orthographicWidth + enemySize.x, orthographicWidth - enemySize.x),
                                             Random.Range(cam.orthographicSize - enemySize.y, -maxYPatrol + enemySize.y));
            if (waitTime <= 0)
            {
                point.position = RandomPoint;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void FollowEnemy()
    {
        if (!enemy)
        {
            Debug.LogWarning("No enemy transform assigned to use SquadMode");
            squadMode = false;
            return;
        }

        if (Vector2.Distance(transform.position, enemy.position) < stopFollowDistance)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
        }
    }

    void LookToPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(transform.position - player.position, Vector3.back);
        newRotation.x = 0f;
        newRotation.y = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * enemySpeedRotation);
    }



}
