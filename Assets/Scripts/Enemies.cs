using System.Collections;
using UnityEngine;

public class Enemies : Character
{
    [SerializeField]
    GameObject[] drops;

    [Space(10), Header("Behaviour")]
    [SerializeField] float minDistance = 10;
    [SerializeField] float midDistance = 6;
    [SerializeField] float visionAngle = 45;
    [SerializeField] float bulletDectection = 5;
    [SerializeField] float timeToRefillFuel = 40;
    [Header("Dash")]
    [SerializeField, Range(0, 1)] float dashProbability;
    [SerializeField] float dashCheckTime = 5;
    [Tooltip("Determines a range of posible directions to dash to when is on under attack. 0 represents an opposite direction to the danger source.")]
    [SerializeField, Range(0, 180)] float dashAngle = 45;

    bool dashChecking = false;

    [SerializeField] GameObject ShotPoint;

    Player player;
    HUD hud;
    EnemiesPool pool;
    int item = 0;
    bool refillingFuel = false;

    public static int enemiesKilled;

    protected override void Awake()
    {
        base.Awake();

        pool = FindObjectOfType<EnemiesPool>();
        player = FindObjectOfType<Player>();
        hud = FindObjectOfType<HUD>();
    }

    protected override void Start()
    {
        base.Start();

        StartCoroutine(CheckDash());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDashig || player.pause) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            Attacking(Attack.slice, ShotPoint.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            
            Attacking(Attack.shoot, ShotPoint.transform.position);
        }

        if (Health <= 0)
        {
            enemiesKilled += 1;
            hud.Score(enemiesKilled);
            Drop();
            pool.Kill(this);
            return;
        }

        var dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis <= minDistance)
        {
            attacks = attackType;
            Attacking(attackType, ShotPoint.transform.position, (player.transform.position - ShotPoint.transform.position).normalized);
            rig.velocity = new Vector3(0, rig.velocity.y, 0);
        }
        else if (dis > minDistance && dis < midDistance)
        {
            attacks = attackType;
            Attacking(attackType, ShotPoint.transform.position, (player.transform.position - ShotPoint.transform.position).normalized);

            var dir = player.transform.position - transform.position;
            MoveTo(dir);

            if (dir.y > 0)
                Fly();
        }
        else if (dis > midDistance)
        {
            var dir = player.transform.position - transform.position;
            MoveTo(dir);

            if (dir.y > 0)
                Fly();
        }

        if (player)
            transform.forward = (player.transform.position - transform.position).normalized;

        if (Fuel <= 0 && !refillingFuel)
        {
            StartCoroutine(RefillFuel());
        }
    }

    private bool DetectBulletsAhead(out Vector3 enemyPosition)
    {
        enemyPosition = Vector3.zero;
        Ray ray = new Ray(transform.position, transform.forward * bulletDectection);
        var collitions = Physics.SphereCastAll(ray, bulletDectection);

        if (collitions == null || collitions.Length <= 0)
            return false;

        foreach (var collition in collitions)
        {
            var bullet = collition.collider.gameObject.GetComponent<bullet>();
            
            if (bullet && bullet.EnemyTag == "Enemies")
            {
                enemyPosition = collition.transform.position;
                return true;
            }
        }

        return false;
    }

    private void Drop()
    {
        item = Random.Range(0, 100);

        if (item <= 20)
        {
            Instantiate(drops[0], transform.position, Quaternion.identity);
        }
        else if (item > 20 && item <= 45)
        {
            Instantiate(drops[1], transform.position, Quaternion.identity);
        }
        else if (item > 45 && item <= 75)
        {
            Instantiate(drops[2], transform.position, Quaternion.identity);
        }
        else if (item > 75)
        {
           
        }

    }

    IEnumerator CheckDash()
    {
        dashChecking = true;

        while (dashChecking)
        {
            var enemyPosition = Vector3.zero;
            if (DetectBulletsAhead(out enemyPosition))
            {
                if (Random.Range(0, 1) <= dashProbability)
                {
                    var angle = Random.Range(0, dashAngle);
                    angle *= Random.value < .5f ? -1 : 1;
                    Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * (transform.position - enemyPosition);
                    direction.Normalize();
                    direction *= 5;
                    //Debug.DrawRay(transform.position, direction, Color.red, 10);

                    if (!IsDashig)
                        Dash(direction);
                }
            }

            yield return new WaitForSeconds(dashCheckTime);
        }

        dashChecking = false;
    }

    IEnumerator RefillFuel()
    {
        refillingFuel = true;

        yield return new WaitForSeconds(timeToRefillFuel);

        Fuel = maxFuel;
        refillingFuel = false;
    }
}
