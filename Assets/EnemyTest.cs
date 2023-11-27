using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest2 : Character
{
    [Header("Behaviour")]
    [SerializeField] float minDistance = 10;
    Player player;

    protected override void Awake()
    {
        base.Awake();

        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        var dis = Vector3.Distance(transform.position, player.transform.position);

        if (dis > minDistance)
        {
            MoveTo(player.transform.position);
        }

        if (Ammo > 0)
        {
            Attacking(Attack.shoot);
        }
    }
}
