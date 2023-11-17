using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Character
{
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
    }
}
