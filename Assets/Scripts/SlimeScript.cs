using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private bool isMovingRight = true;
    public LayerMask wallLayer;

    private bool isCollidingWithBox = false;
    private bool isCollidingWithWall = false;

    private void Update()
    {
        // Cast a ray in the direction of movement to check for walls
        Vector2 rayDirection = isMovingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 0.1f, wallLayer);

        // If the ray hits a wall, change direction
        if (hit.collider != null)
        {
            isMovingRight = !isMovingRight;
            // Flip the enemy's sprite horizontally to match the new direction

        }

        // Move the enemy
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (isCollidingWithBox && isCollidingWithWall)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            isCollidingWithBox = true;
        }

        if (collision.gameObject.tag == "Wall")
        {
            isCollidingWithWall = true;
        }

        if (collision.gameObject.tag == "BoomBa")
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.tag == "Doorman")
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.tag == "Spike")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Lava")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            isCollidingWithBox = false;
        }

        if (collision.gameObject.tag == "Wall")
        {
            isCollidingWithWall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Edge")
        {
            isMovingRight = !isMovingRight;
        }

    }
}

