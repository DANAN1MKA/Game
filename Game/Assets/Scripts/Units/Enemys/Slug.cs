using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slug : Unit
{
    //Сила прыжка
    [SerializeField] private float MoveSpeed;

    //Сылка на обьект сонтролирующий здоровье игрока
    [SerializeField] private HealthController playerHealth;
    GameObject health;

    private Rigidbody2D rb;

    //направление перемещения 
    private Vector3 direction;

    //анимация
    private SpriteRenderer sprite;

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
        if(onGround) Move();
        
        IsOnGround();
    }

    //проверяем нажодится ли лягушка на земле
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

        //проверяем есть ли перед нами препятствие если есть меняем направление движения
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.3f + transform.right * direction.x, 0.1f);
        if (coliders.Length > 0 && coliders.All(x => !x.GetComponent<Player>())) direction *= -1.0f;

        if (direction.x > 0) sprite.flipX = true; else sprite.flipX = false;

        //Прыжки
        if (onGround)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, MoveSpeed * Time.deltaTime);
        }

    }

    private void Damage()
    {
        playerHealth.GetDamage((int)damage);
        playerHealth.UpdateHealth();
    }

}
