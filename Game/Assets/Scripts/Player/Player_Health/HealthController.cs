using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Text healthText;

    public void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthText.text = playerHealth.ToString("0");

        if (playerHealth > maxHealth) playerHealth = maxHealth;
        if (playerHealth <= 0) Application.LoadLevel(Application.loadedLevel);
    }

    public void GetDamage(int damag)
    {
        playerHealth -= damag;
    }

    public void GetHealing(int heal)
    {
        playerHealth += heal;
    }
}
