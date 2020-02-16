using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogg : Unit
{
    [SerializeField]
    private float jumpForce;


    [SerializeField]
    private HealthController playerHealth;

    private Rigidbody2D rb;

    private SpriteRenderer sprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        //StartCoroutine(Move());
    }

    void Update()
    {
        //Move();
        IsOnGround();
       Debug.Log(onGround);
    }

    public LayerMask ground;
    private bool onGround = false;
    private void IsOnGround()
    {
        onGround = Physics2D.OverlapCircle(transform.position, 0.1f, ground);
    }


    private void OnTriggerEnter2D(Collider2D colision)
    {
        if(colision.CompareTag("Player"))
        {
            Damage();
        }
    }

    [SerializeField] private float MoveDelay;

    private void Move()
    {
        //while (true)
        if (onGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            rb.AddForce(transform.right * jumpForce, ForceMode2D.Impulse);

                //yield return new WaitForSeconds(MoveDelay);
        }
    }

    private void Damage()
    {
        playerHealth.GetDamage((int)damage);
        playerHealth.UpdateHealth();
    }
}
