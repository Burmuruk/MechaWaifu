using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Movimiento_P : MonoBehaviour
{
    public float speed = 5f;

    public float dashSpeed;

    Rigidbody rig;

    bool isDashing;

    public int time = 1;
    int count = 0;
        
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            speed = 100f;
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float VerticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0, VerticalInput);
            movementDirection.Normalize();

            transform.position = transform.position + movementDirection * speed * Time.deltaTime;

            //if (movementDirection != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.deltaTime);
        }

        speed = 5f;

        if (Input.GetKeyDown(KeyCode.A))
               isDashing = true;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dashing();

            count++;
            if (count == time)
            {
               gameObject.GetComponent<Collider>().enabled = true;
               rig.useGravity = true;
            }

            count = 0;
        }

        
           
    }

    private void Dashing()
    {
        rig.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);

        rig.useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;

        isDashing = false;
    }
}



