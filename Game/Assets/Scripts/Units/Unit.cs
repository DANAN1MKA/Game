using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected int damage;

    public virtual void ReceiveDamage(float damag)
    {
        health -= damag;
        if (health <= 0) Die();
    }

    public virtual void Die() { Destroy(this.gameObject); }
}

