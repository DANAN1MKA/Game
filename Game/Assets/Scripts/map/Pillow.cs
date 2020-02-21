using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D colision)
    {
        colision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
