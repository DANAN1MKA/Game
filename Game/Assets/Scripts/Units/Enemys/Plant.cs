using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Unit
{
    //[SerializeField] private float damage;

    //Сылка на обьект сонтролирующий здоровье игрока
    [SerializeField] private HealthController playerHealth;
    GameObject health;

    //направление 
    private Vector3 direction;


    //анимация
    private Animator anime;
    private SpriteRenderer sprite;


    //задержка между выстрелами
    [SerializeField] private float shootCooldown;
    private float shootDeltatime;
    
    //префаб пули
    private EnemyBullet bullet;

    //есть ли игрок в поле видимости
    private bool isPlayerInRange = false;

    void Awake()
    {
        anime = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        health = GameObject.Find("HealthController");
        playerHealth = health.GetComponent<HealthController>();

        
        bullet = Resources.Load<EnemyBullet>("EnemyBullet");

    }

    void Start()
    {
        direction = transform.right * -1;
        shootDeltatime = Time.time;
    }

    void Update()
    {
        if (isPlayerInRange && (shootDeltatime + shootCooldown < Time.time))
        {
            shootDeltatime = Time.time;
            shoot();
        }
        //Debug.Log("isPlayerInRange" + isPlayerInRange);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))

            isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))

            isPlayerInRange = false;
    }

    private void shoot()
    {
        Vector3 position = transform.position;
        Debug.Log("pos" + position);
        EnemyBullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as EnemyBullet;
        Debug.Log(newBullet);
        newBullet.Damage = damage;

        Debug.Log("damage"+ damage);

        newBullet.Direction = newBullet.transform.right * (!sprite.flipX ? -1.0f : 1.0f);
        Debug.Log("Direction");
        newBullet.playerHealthController = playerHealth;
    }
}
