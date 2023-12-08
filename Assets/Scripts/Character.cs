using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Pollo
    [Header("Movement")]
    public float speed = 5f;
    public float flySpeed = .5f;
    public float dashSpeed;
    [SerializeField] bool isDashing;
    public int time = 1;
    float count = 0;
    protected Rigidbody rig;

    public bool IsDashig { get => isDashing; private set => isDashing = value; }
    #endregion

    #region Boi
    [Space(10), Header("Health")]
    [SerializeField] int health = 3;
    [SerializeField, Range(0, 10)] int healthRange = 3;
    [SerializeField] int minHealth = 1;

    [Space(10), Header("Attack")]
    [SerializeField]
    [Tooltip("Bullet to instanciate.")]
    GameObject bullets;
    [SerializeField]
    protected GameObject sword;
    protected Attack attacks;
    int dmg = 1;
    [SerializeField] int ammo = 10;
    [SerializeField] protected int maxAmmo = 10;
    [SerializeField] float fuel = 100;
    [SerializeField] protected float maxFuel = 100;
    [SerializeField] float fuelConsuption = 2;

    public int Ammo
    {
        get => ammo;
        set
        {
            ammo = value;
            OnAmmoChanged?.Invoke(value);
        }
    }

    public int Health
    {
        get => health;
        set
        {
            health = value;
            OnHealthChanged?.Invoke(value);
        }
    }

    public float Fuel
    {
        get => fuel;
        set
        {
            fuel = value;
            OnFuelChanged?.Invoke(value);
        }
    }

    protected enum Attack
    {
        slice, shoot
    }

    public event Action<float> OnHealthChanged;
    public event Action<int> OnAmmoChanged;
    public event Action<float> OnFuelChanged;
    #endregion

    #region Burmuruk
    [SerializeField] protected Attack attackType = Attack.shoot;
    [SerializeField] float attackRate = 1;
    bool canAttack = true;
    #endregion

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        if (sword)
        {
            sword.gameObject.SetActive(false);
        }

        if (healthRange != 0)
            health = UnityEngine.Random.Range(minHealth, healthRange + 1);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            count += Time.deltaTime;
            if (count >= time)
            {
                gameObject.GetComponent<Collider>().enabled = true;
                rig.useGravity = true;
                isDashing = false;
                count = 0;
            }
        }
    }

    public virtual void Damage()
    {
        Health -= dmg;
    }

    protected void Fly(bool value = true)
    {
        if (fuel <= 0) return;

        if (value)
        {
            rig.velocity = new()
            {
                x = rig.velocity.x,
                y = flySpeed,
                z = rig.velocity.z,
            };

            Fuel -= fuelConsuption * Time.deltaTime;
        }
        //else
        //    speed = 5f;
    }

    protected void Dash(Vector3 direction)
    {
        isDashing = true;

        rig.AddForce(direction.normalized * dashSpeed, ForceMode.Impulse);

        rig.useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    protected void MoveTo(Vector3 direction)
    {
        Vector3 movementDirection = new Vector3(direction.x, 0, direction.z);
        movementDirection.Normalize();
        var destiny = transform.position + movementDirection;
        var velocity = (destiny - transform.position).normalized * speed;

        rig.velocity = new()
        {
            x = velocity.x,
            y = rig.velocity.y,
            z = velocity.z,
        };

        //transform.forward = direction;
        //if (movementDirection != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.deltaTime);
    }

    protected void Attacking(Attack attackType, Vector3 shootPoint, Vector3 direction = default)
    {
        if (isDashing || !canAttack) return;

        switch (attackType)
        {
            case Attack.slice:
                if (!sword.gameObject.activeSelf)
                    StartCoroutine(ActivateCollider());
                break;
            case Attack.shoot:
                if (bullets && Ammo != 0)
                {
                    print("shoot");
                    var bullet = Instantiate(bullets, shootPoint, Quaternion.identity);
                    bullet.transform.forward = direction == default ? transform.forward : direction;
                    Ammo -= 1;
                }
                break;
            default:
                break;
        }

        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackRate);

        canAttack = true;
    }

    IEnumerator ActivateCollider()
    {
        sword.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        sword.gameObject.SetActive(false);
    }
}
