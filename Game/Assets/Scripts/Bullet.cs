using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private float damage;
    public float Damage { set { damage = value; } }

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start()
    {
        Destroy(gameObject, 1.4f);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Enemy"))
        {
            Unit enemy = colision.GetComponent<Unit>();
            enemy.ReceiveDamage(damage);
            GameObject.Destroy(this.gameObject);
        }
    }
}
