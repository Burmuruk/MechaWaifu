using System;
using UnityEngine;

public class Player : Character
{
    Vector3 direction = default;

    public Vector3 CameraNormal { get; set; }

    private void Update()
    {
        if (IsDashig) return;

        if (Input.GetKey("space") || Input.GetButton("Fly"))
            Fly();

        direction = new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            y = 0,
            z = Input.GetAxis("Vertical"),
        };

        //direction = Vector3.ProjectOnPlane(direction, CameraNormal);

        if (Input.anyKeyDown)
        {
            var names = Input.GetJoystickNames();

            foreach (var name in names)
            {
                print(name);
            }
        }
        
        //if (Input.GetButtonDown("Fire1"))
        //    print("button 0");

        //if (Input.GetButtonDown("Fire2"))
        //    print("button 1");

        //if (Input.GetButtonDown("Fire3"))
        //    print("button 2 ");

        //if (Input.GetButtonDown("Shoot"))
        //    print("button 3");

        MoveTo(direction);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Dash(direction);

        if (Input.GetMouseButtonDown(0))
        {
            Attacking(Attack.slice);
            Time.timeScale = 0f;
        }

        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("Shoot"))
        {
            Attacking(Attack.shoot);
        }
    }
}
