using UnityEngine;

public class Giro : MonoBehaviour
{
    [SerializeField]float speed;
    [SerializeField] float gap =.5f;
    Vector3 curRotation = default;
    public float zOffset = 130;
    [SerializeField] Vector3 offsetPos = default;
    [SerializeField] Vector3 startRotation = new Vector3(180, 180, 0);
    Vector3 acel = default;
    float yInicial;
    float calY;
    bool started = false;
    Quaternion rot;

    GameObject cameraContainer;
    Gyroscope gyro;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        curRotation = Input.acceleration;
        Input.gyro.enabled = true;

        cameraContainer = new GameObject("Camera container");
        //cameraContainer.transform.position = transform.position;
        cameraContainer.transform.SetParent(transform.parent);
        cameraContainer.transform.position = new (0, 1f, 0);
        transform.SetParent(cameraContainer.transform);
        transform.position = offsetPos;
        
        gyro = Input.gyro;
        EnableGyro();
    }

    private void EnableGyro()
    {
        gyro.enabled = true;
        cameraContainer.transform.rotation = Quaternion.Euler(startRotation);
        rot = new Quaternion(0, 0, 1, 0);

    }

    void Update()
    {

        Quaternion rotation = Input.gyro.attitude * rot;
        transform.localRotation = rotation;
        player.CameraForward = transform.forward;
        player.CameraNormal = Vector3.Cross(transform.forward, transform.right);
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
