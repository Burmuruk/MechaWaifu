using System;
using UnityEngine;

public class Player : Character
{
    Vector3 direction = default;

    public Vector3 CameraForward { get; set; }
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

        var angle = Vector3.SignedAngle(new(Vector3.forward.x, 0, Vector3.forward.z), new(CameraForward.x, 0, CameraForward.z), Vector3.up);
        Vector3 rotatedDir = Quaternion.AngleAxis(angle, Vector3.up) * direction;

        //Debug.DrawRay(transform.position , rotatedDir * 3, Color.red);

        if (direction != default)
            MoveTo(rotatedDir);

        transform.forward = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;

        //if (Input.GetButtonDown("Fire1"))
        //    print("button 0");

        //if (Input.GetButtonDown("Fire2"))
        //    print("button 1");

        //if (Input.GetButtonDown("Fire3"))
        //    print("button 2 ");

        //if (Input.GetButtonDown("Shoot"))
        //    print("button 3");

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash"))
        {
            Dash(rotatedDir);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Hit"))
        {
            Attacking(Attack.slice);
        }

        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("Shoot"))
        {
            Attacking(Attack.shoot, CameraForward);
            Debug.DrawRay(transform.position, CameraForward);
        }
    }
}
