using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 targetPosition = new Vector2(0, 0);

    public float speed = 5;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

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
        animator = GetComponent<Animator>();
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
            SetAnimeDirection(0, transform.position.y);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            SetAnimeDirection(transform.position.x, 0);
        }
        
    }

    public void SetAnimeDirection(float x, float y)
    {
        animator.SetFloat("Horizontal", -x);
        animator.SetFloat("Vertical", -y);
    }


}
