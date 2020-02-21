using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDeal : MonoBehaviour
{
    [SerializeField] private int damageValue;
    [SerializeField] private HealthController healthController;
    GameObject health;

    void Awake()
    {
        health = GameObject.Find("HealthController");
        healthController = health.GetComponent<HealthController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        p.jump();
        if (collision.CompareTag("Player")) Damage();
    }

    private void Damage()
    {
        healthController.GetDamage(damageValue);
        healthController.UpdateHealth();
        //this.gameObject.SetActive(false);
    }
}
