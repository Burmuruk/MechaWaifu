using UnityEngine;

public class picking : MonoBehaviour
{
    public int ammo = 5;
    public int health = 5;
    public int fuel = 20;

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
                    other.GetComponent<Character>().Ammo += ammo;
                    Destroy(gameObject);
                    break;
                case Drops.health:
                    other.GetComponent<Character>().Health += health;
                    Destroy(gameObject);
                    break;
                case Drops.fuel:
                    other.GetComponent<Character>().Fuel += fuel;
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
