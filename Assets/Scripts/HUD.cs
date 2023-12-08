using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Image healthBar;
    public Image fuelBar;

    public float healthAmount = 100f;
    public float fuelAmount = 100f;

    public TextMeshProUGUI bulletCount;
    public TextMeshProUGUI score;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        player.OnHealthChanged += ShowHealth;
        player.OnAmmoChanged += BulletCount;
        player.OnFuelChanged += ShowFuel; 
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletCount.text = player.Ammo.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowHealth(float health)
    {
        healthBar.fillAmount = health / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

    public void ShowFuel(float fuel)
    {
        fuelAmount -= fuel;
        fuelBar.fillAmount = fuel / 100f;
    }

    public void RechargeFuel(float rechargingAmount)
    {
        fuelAmount += rechargingAmount;
        fuelAmount = Mathf.Clamp(fuelAmount, 0, 100);

        fuelBar.fillAmount = fuelAmount / 100f;
    }

    public void BulletCount(int bullets)
    {
        bulletCount.text = bullets.ToString();
    }

    public void Score(int enemiesKilled)
    {
        score.text = enemiesKilled.ToString();
    }
}
