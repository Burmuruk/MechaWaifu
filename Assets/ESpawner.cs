using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawner : MonoBehaviour
{
    EnemiesPool pool;
    [SerializeField] GameObject[] distancePoints;
    [SerializeField] GameObject[] nearPoints;
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
            en.transform.position = distancePoints[UnityEngine.Random.Range(0, distancePoints.Length)].transform.position; 
        }

        for (int i = 0; i < near; i++)
        {
            en = pool.GetEnemy(EnemyType.Melee);
            en.transform.position = nearPoints[UnityEngine.Random.Range(0, nearPoints.Length)].transform.position; 
        }
    }

    void Update()
    {
        
    }
}
