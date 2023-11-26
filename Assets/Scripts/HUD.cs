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
    public int bullets = 10;

    // Start is called before the first frame update
    void Start()
    {
        bulletCount.text = "00";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UseFuel(10);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RechargeFuel(10);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bullets -= 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            bullets += 1;
        }
        BulletCount();
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

    public void UseFuel(float fuelUsed)
    {
        fuelAmount -= fuelUsed;
        fuelBar.fillAmount = fuelAmount / 100f;
    }

    public void RechargeFuel(float rechargingAmount)
    {
        fuelAmount += rechargingAmount;
        fuelAmount = Mathf.Clamp(fuelAmount, 0, 100);

        fuelBar.fillAmount = fuelAmount / 100f;
    }

    public void BulletCount()
    {
        bulletCount.text = bullets.ToString();
    }
}
