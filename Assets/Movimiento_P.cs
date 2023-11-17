using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_P : MonoBehaviour
{
    public float speed = 5f;

    //float rotationSpeed = 5f;
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
    }
}

