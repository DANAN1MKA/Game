using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player")) Application.LoadLevel(Application.loadedLevel);
    }
}
