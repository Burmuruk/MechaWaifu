using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] string enemyTag = "Player";
    float speed = .1f;
    float timer = 3000f;
    float count = 0f;
    Player player;
    Vector3 direction = Vector3.zero;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        direction = (player.transform.position - transform.position).normalized;
    }

    public string EnemyTag { get => enemyTag; }

    void Update()
    {
        transform.position += direction * speed;
        count += Time.deltaTime;
        if (count>timer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        {
            print(other.transform.name);
            other.GetComponent<Character>().Damage();
            Destroy(gameObject);
        }
    }
}
