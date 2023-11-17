using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Giro : MonoBehaviour
{
    [SerializeField]float speed;
    [SerializeField] float gap =.5f;
    Vector3 curRotation = default;
    public float zOffset = 130;
    Vector3 offset = default;
    Vector3 acel = default;
    Vector3 newRotation = default;
    float yInicial;
    float calY;
    bool started = false;
    Quaternion rot;

    GameObject cameraContainer;
    Gyroscope gyro;


    // Start is called before the first frame update
    void Start()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        curRotation = Input.acceleration;
        Input.gyro.enabled = true;

        cameraContainer = new GameObject("Camera container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
        
        gyro = Input.gyro;
        EnableGyro();
    }

    private void EnableGyro()
    {
        gyro.enabled = true;
        cameraContainer.transform.rotation = Quaternion.Euler(180, 90, 0);
        rot = new Quaternion(0, 0, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //var dis = curRotation.magnitude - Input.acceleration.magnitude ;
        //print(dis);

        //var hi = Input.acceleration;
        //MoveCamera(hi);
        //curRotation = hi;
        //var cel = Input.gyro.attitude.eulerAngles;
        //acel = Input.gyro.userAcceleration;
        //acel.Normalize();

        //if (Input.touchCount > 0)
        //{
        //    offset = cel;
        //}

        //print(Input.gyro.userAcceleration.normalized);
        //transform.rotation = Quaternion.Euler((cel.y - offset.y) , (cel.x - offset.x) * acel.y * speed * -1, cel.z - offset.z);
        //transform.rotation = Quaternion.Euler(cel.x, cel.y * -1, cel.z + 180);





        //transform.rotation = Input.gyro.attitude;
        ////transform.rotation = Quaternion.Euler((cel.y - offset.y) , (cel.x - offset.x) * acel.y * speed * -1, cel.z - offset.z);
        //transform.Rotate(0, 0, 180, Space.Self);
        //transform.Rotate(90, 180, 0, Space.World);

        //if (started)
        //{
        //    System.Timers.Timer hu = new System.Timers.Timer(3000);
        //    hu.AutoReset = false;
        //    hu.Elapsed += delegate { calY = transform.eulerAngles.y - yInicial; };
        //}




        transform.localRotation = Input.gyro.attitude * rot;





        //if (Input.gyro.attitude.eulerAngles is var r && MathF.Abs(curRotation.magnitude - r.magnitude) > gap)
        //{
        //    MoveCamera(r);
        //    curRotation = r;
        //}
    }

    private void MoveCamera(Vector3 rotation)
    {
        var newRotarion = new Vector3
        {
            x = rotation.x - curRotation.x,
            y = 0,
            z = 0
        };
        //Mathf.Abs(rotation.y) - Mathf.Abs(curRotation.y)
        print(newRotarion.normalized);

        transform.rotation = Quaternion.Euler(transform.rotation.x + newRotarion.normalized.x * speed, 0, 0);/* ;*/
    }
}
