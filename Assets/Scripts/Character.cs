using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected void Start()
    {
        
    }

    int dmg = 1;
    int health = 3;
    public void Damage()
    {
        health -= dmg;

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    protected void Fly()
    {

    }

    bool isDashing = false;

    protected void Dash()
    {

    }

    protected void Movement()
    {

    }

    [SerializeField]
    GameObject bullets;

    [SerializeField]
    protected GameObject sword;

    protected enum Attack
    {
        slice, shoot
    }
    protected Attack attacks;
    protected void Attacking()
    {
        if (!isDashing)
        {
            switch (attacks)
            {
                case Attack.slice:
                    if (!sword.gameObject.activeSelf)
                        StartCoroutine(ActivateCollider());
                    break;
                case Attack.shoot:
                    if (bullets)
                        Instantiate(bullets, transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator ActivateCollider()
    {
        sword.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        sword.gameObject.SetActive(false);
    }
}
