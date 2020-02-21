using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Opossum : Unit
{
    //Скорость перемещения
    [SerializeField] private float moveSpeed;

    //Сылка на обьект сонтролирующий здоровье игрока
    [SerializeField] private HealthController playerHealth;

    private Rigidbody2D rb;

    //направление перемещения 
    private Vector3 direction;

    //анимация
    private SpriteRenderer sprite;

    GameObject health;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        health = GameObject.Find("HealthController");
        playerHealth = health.GetComponent<HealthController>();

    }

    void Start()
    {
        direction = transform.right;
    }

    void Update()
    {
        Move();
        IsOnGround();
    }

    //проверяем нажодится ли опоссум на земле
    #region
    public LayerMask ground;
    private bool onGround = false;

    private void IsOnGround()
    {
        onGround = Physics2D.OverlapCircle(transform.position, 0.01f, ground);
    }
    #endregion

    //наносим урон игроку
    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            Player tmp = colision.GetComponent<Player>();
            tmp.ReciveDamage(direction);

            Damage();
        }
    }

    private void Move()
    {

        //проверяем есть ли перед нами есть стена или обрыв 
        //если есть меняем направление движения
        Collider2D[] wall = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x, 0.1f);
        Collider2D[] ground = Physics2D.OverlapCircleAll(transform.position + transform.right * direction.x, 0.1f);
        //if (ground.Length < 1) direction *= -1.0f;
        if ((wall.Length > 0 && 
            wall.All(x => !x.GetComponent<Player>()) && 
            wall.All(x => !x.GetComponent<Bullet>())) ||
            ground.Length < 1)
            direction *= -1.0f;

        if (direction.x > 0) sprite.flipX = true; else sprite.flipX = false;


        //ходьба
        if (onGround)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
        }

    }

    private void Damage()
    {
        playerHealth.GetDamage((int)damage);
        playerHealth.UpdateHealth();
    }
}

