using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private bool right;

    private Rigidbody2D rb_block;
    private Animator anime;

    void Awake()
    {
        rb_block = GetComponentInChildren<Rigidbody2D>();
        anime = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D colision)
    {
        if(colision.CompareTag("Player"))
        {
            rb_block.bodyType = RigidbodyType2D.Dynamic;
            rb_block.AddForce(transform.right * (right ? 1 : -1) * force, ForceMode2D.Impulse);

            anime.SetBool("state", !anime.GetBool("state"));
        }
    }
}
