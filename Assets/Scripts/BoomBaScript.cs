using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBaEnemy : MonoBehaviour
{
    public Transform target;
    public float detectionDistance = 5f;
    public float moveSpeed = 2.0f;
    private bool isMovingRight = true;

    private void Update()
    {
        // Cast a ray in the direction of movement to check for walls
        Vector2 rayDirection = isMovingRight ? Vector2.right : Vector2.left;

        // Move the enemy
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (target == null)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            ///Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Slime")
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.tag == "BoomBa")
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.tag == "Doorman")
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.tag == "Lava")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Spikes")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Edge")
        {
            isMovingRight = !isMovingRight;
        }

    }
}
