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

    //Джостик
    [SerializeField] private Joystick control;

    /*[SerializeField]*/ private Bullet bullet;


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
    }

    void Update()
    {
        //if (onGround) state = CharState.idle;
        if (control.Horizontal != 0 || Input.GetAxis("Horizontal") != 0) move();
        else if (onGround) state = CharState.idle;

        //Клавиатура
        if (Input.GetButtonDown("Jump")) jump();

        if (Input.GetButtonDown("Fire1")) StartCoroutine( Shooting());

    }

    void FixedUpdate()
    {
        IsOnGround();

    }

    //Проверка но соприкосновение с землёй
    #region
    public Transform checkGround;
    public LayerMask ground;
    private bool onGround = false;
    private void IsOnGround()
    {
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

        //клавиатура
        if (Input.GetButton("Horizontal")) moveVector = Vector3.right * Input.GetAxis("Horizontal");

        //разварачиваем персонажа в нужную сторону
        flipCheracter.flipX = moveVector.x < 0;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveVector, moveSpeed * Time.deltaTime);

    }


    [SerializeField] float reloadingTime;
    public void shoot()
    {
        Vector3 position = transform.position;
        Bullet newBullet =  Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Damage = bulletDamage;
        newBullet.Direction = newBullet.transform.right * (flipCheracter.flipX ? -1.0f : 1.0f);
    }

    public IEnumerator Shooting()
    {
        while (Input.GetButton("Fire1"))
        {
            shoot();
            yield return new WaitForSeconds(reloadingTime);
        }

    }
}