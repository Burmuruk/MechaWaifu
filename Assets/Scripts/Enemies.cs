using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Character
{
    [SerializeField]
    GameObject[] drops;
    EnemiesPool pool;
    int item = 0;

    protected override void Awake()
    {
        base.Awake();

        pool = FindObjectOfType<EnemiesPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            attacks = Attack.slice;
            Attacking();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            attacks = Attack.shoot;
            Attacking();
        }

        if (Health <= 0)
        {
            Drop();
            pool.Kill(this);
        }
    }

    private void Drop()
    {
        item = Random.Range(0, 100);

        if (item <= 20)
        {
            Instantiate(drops[0], transform.position, Quaternion.identity);
        }
        else if (item > 20 && item <= 50)
        {
            Instantiate(drops[1], transform.position, Quaternion.identity);
        }
        else if (item > 50)
        {
            Instantiate(drops[2], transform.position, Quaternion.identity);
        }
    }
}
