using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] string enemyTag = "Player";
    [SerializeField] float speed = .1f;
    [SerializeField] float timer = 3000f;
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
        if (player.pause) return;

        //transform.position += direction * speed;
        transform.position += transform.forward * speed * Time.deltaTime;

        count += Time.deltaTime;
        if (count>timer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<bullet>() || (other.GetComponent<Enemies>() || other.GetComponent<Player>()) && other.tag != enemyTag) return;

        if (other.tag == enemyTag)
        {
            other.GetComponent<Character>().Damage();
        }

        Destroy(gameObject);
    }
}
