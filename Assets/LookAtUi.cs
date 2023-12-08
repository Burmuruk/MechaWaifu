using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtUi : MonoBehaviour
{
    [SerializeField] float distance = 10;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + player.transform.forward * distance;
    }
}
