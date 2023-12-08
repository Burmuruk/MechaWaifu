using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawner : MonoBehaviour
{
    EnemiesPool pool;
    [SerializeField] GameObject[] distancePoints;
    [SerializeField] GameObject[] nearPoints;
    [SerializeField] Enemy[] enemies;
    [SerializeField] float spawnTime = 60;
    [SerializeField] int maxCount = 100;

    [Serializable]
    public struct Enemy
    {
        public EnemyType type;
        public int count;
    }

    private void Awake()
    {
        pool = FindObjectOfType<EnemiesPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        GameObject en;

        while (true)
        {
            foreach (Enemy enemy in enemies)
            {
                for (int i = 0; i < enemy.count; i++)
                {
                    if (pool.count >= maxCount && pool.Availables <= 0)
                        yield break;

                    en = pool.GetEnemy(enemy.type);
                    en.transform.position = distancePoints[UnityEngine.Random.Range(0, distancePoints.Length)].transform.position;
                }
            } 

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
