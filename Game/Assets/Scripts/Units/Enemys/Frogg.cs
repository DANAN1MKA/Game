using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Frogg : Unit
{
    //Сила прыжка
    [SerializeField] private float jumpForce;

    //Сылка на обьект сонтролирующий здоровье игрока
    [SerializeField] private HealthController playerHealth;
    GameObject health;

    private Rigidbody2D rb;

    //направление перемещения 
    private Vector3 direction;

    //анимация
    private Animator anime;
    private SpriteRenderer sprite;

    //задержка между прыжками
    [SerializeField] private float jumpingCooldown;
    private float jumpingDeltatime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anime = GetComponentInChildren<Animator>();

        health = GameObject.Find("HealthController");
        playerHealth = health.GetComponent<HealthController>();

    }

    void Start()
    {
        direction = transform.right;
        jumpingDeltatime = Time.time;
    }

    void Update()
    {
        if (Time.time > (jumpingDeltatime + jumpingCooldown))
        {
            jumpingDeltatime = Time.time;
            Move();
        }
        IsOnGround();
    }

    //проверяем нажодится ли лягушка на земле
    #region
    public LayerMask ground;
    private bool onGround = false;

    private void IsOnGround()
    {
        onGround = Physics2D.OverlapCircle(transform.position, 0.01f, ground);
        if (!onGround) anime.SetInteger("state", 1);
        else anime.SetInteger("state", 0);
    }
    #endregion

    //наносим урон игроку
    private void OnTriggerEnter2D(Collider2D colision)
    {
        if(colision.CompareTag("Player"))
        {
            Player tmp = colision.GetComponent<Player>();
            tmp.ReciveDamage(direction);

            Damage();
        }
    }

    private void Move()
    {

        //проверяем есть ли перед нами препятствие если есть меняем направление движения
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x, 0.1f);
        if (coliders.Length > 0 && coliders.All(x => !x.GetComponent<Player>())) direction *= -1.0f;

        if (direction.x > 0) sprite.flipX = true; else sprite.flipX = false;


        //ходьба
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, jumpForce * Time.deltaTime);

        //Прыжки
        if (onGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            rb.AddForce(transform.right * (jumpForce /2) * direction.x, ForceMode2D.Impulse);
        }
       
    }

    private void Damage()
    {
        playerHealth.GetDamage((int)damage);
        playerHealth.UpdateHealth();
    }
}
