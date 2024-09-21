using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 targetPosition = new Vector2(0, 0);

    public float speed = 5;

    private SpriteRenderer spriteRenderer;

    public enum enemyState
    {
        active,
        inactive
    }

    public enemyState state;

    // Start is called before the first frame update
    void Start()
    {
        state = enemyState.active;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerDead += OnPlayerDead;
    }

    public void OnPlayerDead(object sender, EventArgs e)
    {
        state = enemyState.inactive;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == enemyState.inactive) return;

        if(transform.position.y > 0 || transform.position.y < 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, (speed/2f) * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        
    }


}
