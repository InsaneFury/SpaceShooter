using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [Header("Settings")]
    public List<Pool> pools;
    public Dictionary<string,Queue<GameObject>> poolDictionary;
    public float shootPower = 100f;
    public float fireRate = 2f;

    float timeToFire = 0f;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    void FixedUpdate()
    {
        if (Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    public GameObject SpawnBulletFromPool(string tag, Vector2 pos, Quaternion rot)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    public void Shoot()
    {
        GameObject currentBullet = SpawnBulletFromPool("BadLaser", transform.position, transform.rotation);

        currentBullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        currentBullet.GetComponent<Rigidbody2D>().AddForce(-transform.up * shootPower * Time.fixedUnscaledDeltaTime, ForceMode2D.Impulse);
    }
}
