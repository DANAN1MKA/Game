using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCherry : MonoBehaviour
{
    [SerializeField] private int healValue;
    [SerializeField] private HealthController healthController;
    GameObject health;

    void Awake()
    {
        health = GameObject.Find("HealthController");
        healthController = health.GetComponent<HealthController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) Healing();
    }

    private void Healing()
    {
        healthController.GetHealing(healValue);
        healthController.UpdateHealth();
        this.gameObject.SetActive(false);
    }
}
