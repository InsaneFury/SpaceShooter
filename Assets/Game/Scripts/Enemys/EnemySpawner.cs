using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonobehaviourSingleton<EnemySpawner>
{
    public List<GameObject> enemysGO;
    public GameObject boss;
    public float maxSpawnRateInSeconds = 20f;
    public float IncreaseDifficultyEvery = 40f;
    public int scoreToBossSpawn = 20000;
    ScoreManager sManager;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        sManager = ScoreManager.Get();
        StartSpawning();
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //BOTTOM-LEFT SCREEN POINT
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //TOP-RIGHT SCREEN POINT

        Vector3 RandPos = new Vector3(Random.Range(min.x, max.x), max.y);

        int randEnemy = (int)Random.Range(0f, enemysGO.Count);

        GameObject go = Instantiate(enemysGO[randEnemy], RandPos, Quaternion.identity);
        Enemy[] childsEnemy;
        childsEnemy = go.GetComponentsInChildren<Enemy>();
        foreach (Enemy e in childsEnemy)
        {
            e.OnEnemyDie += sManager.AddScore;
            e.OnEnemyDie += sManager.AddDestroyedEnemy;
        }
        
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 0f)
        {
            maxSpawnRateInSeconds--;
        }

        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    public void SpawnFinalBoss()
    {  
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //BOTTOM-LEFT SCREEN POINT
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //TOP-RIGHT SCREEN POINT

        Vector3 pos = new Vector3(min.x / 2, max.y / 2);

        Instantiate(boss, pos, Quaternion.identity);
    }

    public void StopSpawn()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

    public void StartSpawning()
    {
        SpawnEnemy();
        InvokeRepeating("SpawnEnemy", 0f, maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, IncreaseDifficultyEvery);
    }
}
