using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event EventHandler playerDead;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            playerDead?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}
