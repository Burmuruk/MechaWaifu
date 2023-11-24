using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class picking : MonoBehaviour
{
    public enum Drops
    {
        ammo, health, fuel
    }
    public Drops drop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (drop)
            {
                case Drops.ammo:
                    other.GetComponent<Character>().ammo += 5;
                    Destroy(gameObject);
                    break;
                case Drops.health:
                    other.GetComponent<Character>().health += 1;
                    Destroy(gameObject);
                    break;
                case Drops.fuel:
                    other.GetComponent<Character>().fuel += 20;
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
