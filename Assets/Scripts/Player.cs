using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    Vector3 direction = default;

    [SerializeField]public bool pause = false;

    public Vector3 CameraForward { get; set; }
    public Vector3 CameraNormal { get; set; }

    [SerializeField] GameObject ShotPoint;

    public GameObject pausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pause = true;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pause = false;
                pausePanel.SetActive(false);
            }

        }



        if (IsDashig || pause) return;

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
            Attacking(Attack.slice, ShotPoint.transform.position);
        }

        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("Shoot"))
        {
            Attacking(Attack.shoot, ShotPoint.transform.position, CameraForward);
            Debug.DrawRay(transform.position, CameraForward);
        }
    }
}
