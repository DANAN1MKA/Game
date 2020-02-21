using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private HealthController playerHealth;
    public HealthController playerHealthController { set { playerHealth = value; } }


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
        sprite.flipX = direction.x > 0;
        Destroy(gameObject, 1.4f);
    }

    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            playerHealth.GetDamage((int)damage);
            playerHealth.UpdateHealth();
            GameObject.Destroy(this.gameObject);
        }
    }
}
