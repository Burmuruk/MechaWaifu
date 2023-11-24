using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Character
{
    [SerializeField]
    GameObject[] drops;
    int item = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (sword)
        {
            sword.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            attacks = Attack.slice;
            Attacking();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            attacks = Attack.shoot;
            Attacking();
        }

        if (health <= 0)
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
            Destroy(gameObject);
        }
    }
}
