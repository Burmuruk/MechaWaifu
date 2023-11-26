using System;
using UnityEngine;

public class Player : Character
{
    Vector3 direction = default;

    private void Update()
    {
        if (IsDashig) return;

        if (Input.GetKey("space"))
            Fly();

        direction = new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            y = 0,
            z = Input.GetAxis("Vertical"),
        };

        MoveTo(direction);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Dash(direction);

        if (Input.GetMouseButtonDown(0))
        {
            attacks = Attack.slice;
            Attacking();
        }

        if (Input.GetMouseButtonDown(1))
        {
            attacks = Attack.shoot;
            Attacking();
        }
    }
}
