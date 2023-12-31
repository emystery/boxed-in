using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBaEnemy : MonoBehaviour
{
    public Transform target = null;
    public float detectionDistance = 5f;
    public float moveSpeed = 2.0f;
    private bool isMovingRight = true;

    private SpriteRenderer spriteRenderer;

    private Animator anim;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Box").transform;
        }

        // Cast a ray in the direction of movement to check for walls
        Vector2 rayDirection = isMovingRight ? Vector2.right : Vector2.left;

        // Move the enemy
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = true;
        }

        if (target == null)
        {
            return;
        }

        /*float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionDistance)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Slime"))
        {
            isMovingRight = !isMovingRight;
        }

        if (    collision.gameObject.CompareTag("BoomBa"))
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.CompareTag("Doorman"))
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            isMovingRight = !isMovingRight;
        }

    }
}
