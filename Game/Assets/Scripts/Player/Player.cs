using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharState
{
    idle,
    run,
    jump
}

public class Player : MonoBehaviour
{
    //Характеристики
    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private float jumpForse = 150.0f;

    [SerializeField] private float bulletDamage = 10;

    
    private Rigidbody2D rb;

    //анимации персонажа
    private Animator animation;

    //необходимо для разворота персонажа в необходимую сторону
    private SpriteRenderer flipCheracter;

    private Vector3 direction;

    //Джостик
    [SerializeField] private Joystick control;

    /*[SerializeField]*/ private Bullet bullet;

    //Время перезарядки
    [SerializeField] private float reloadingTime;
    //Время последнего выстрела
    private float deltaReloadingTime;


    //выбираем необходимую анимацию
    private CharState state
    {
        get { return (CharState)animation.GetInteger("state"); }
        set { animation.SetInteger("state", (int)value); }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        flipCheracter = GetComponent<SpriteRenderer>();


        bullet = Resources.Load<Bullet>("Bullet");
    }

    void Start()
    {
        deltaReloadingTime = Time.time;

    }

    void Update()
    {
        if (control.Horizontal != 0 || Input.GetAxis("Horizontal") != 0)
        {
            move();
        }
        else if (onGround) state = CharState.idle;

        //Клавиатура
        if (Input.GetButtonDown("Jump")) jump();

        if (Input.GetButton("Fire1"))
        {
            shoot();
        }

    }

    void FixedUpdate()
    {
        IsOnGround();

    }

    //Проверка но соприкосновение с землёй
    #region
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask ground;
    private bool onGround = false;

    //проверка есть ли перед игроком стена
    [SerializeField] private Transform wallCheck;
    private bool wall;
    private void IsOnGround()
    {
        wall = Physics2D.OverlapCircle(wallCheck.position + transform.right * 0.5f * direction.x, 0.1f, ground);

        onGround = Physics2D.OverlapCircle(checkGround.position, 0.3f, ground);
        if(!onGround) state = CharState.jump;
    }
    #endregion

    //прыжок
    public void jump()
    {
        if (onGround) { rb.AddForce(transform.up * jumpForse, ForceMode2D.Impulse); }
    }

    //Перемещение
    public void move()
    {
        if (onGround) state = CharState.run;

        //джостик
        Vector3 moveVector = Vector3.right * control.Horizontal;
        direction = Vector3.right * control.Horizontal;


        //клавиатура
        if (Input.GetButton("Horizontal"))
        {
            moveVector = Vector3.right * Input.GetAxis("Horizontal");
            direction = Vector3.right * Input.GetAxis("Horizontal");

        }

        //разварачиваем персонажа в нужную сторону
        flipCheracter.flipX = moveVector.x < 0;
        
        if(!wall)
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveVector, moveSpeed * Time.deltaTime);

    }

    public void ReciveDamage(Vector3 direction)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
    }

    public void shoot()
    {
        if (Time.time > (deltaReloadingTime + reloadingTime))
        {
            deltaReloadingTime = Time.time;

            Vector3 position = transform.position;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Damage = bulletDamage;
            newBullet.Direction = newBullet.transform.right * (flipCheracter.flipX ? -1.0f : 1.0f);
        }
    }


    //~Player()
    //{
    //    Application.loadedLevel(Application.loadedLevel);
    //}
}