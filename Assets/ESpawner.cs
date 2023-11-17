using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawner : MonoBehaviour
{
    EnemiesPool pool;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] distancePoints;
    [SerializeField] int dis = 5;
    [SerializeField] int near = 10;

    private void Awake()
    {
        pool = FindObjectOfType<EnemiesPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject en;

        for (int i = 0; i < dis; i++)
        {
            en = pool.GetEnemy(EnemyType.Distance);
            en.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position; 
        }

        for (int i = 0; i < near; i++)
        {
            en = pool.GetEnemy(EnemyType.Melee);
            en.transform.position = distancePoints[UnityEngine.Random.Range(0, distancePoints.Length)].transform.position; 
        }
    }

    void Update()
    {
        
    }
}
