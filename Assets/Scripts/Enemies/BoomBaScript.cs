using System.Collections;
using UnityEngine;

public class BoomBaEnemy : MonoBehaviour
{
    public Transform target = null;
    public float moveSpeed = 2.0f;
    private bool isMovingRight = true;
    private const float delayToKill = 2.0f;


    private SpriteRenderer spriteRenderer;

    public bool alive;

    public Animator animator;

    private void Start()
    {
        alive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (alive)
        {
            Vector2 rayDirection = isMovingRight ? Vector2.right : Vector2.left;

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
        }
        else
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
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
            alive = false;
            animator.SetBool("Killed", true);

            StartCoroutine(WaitForKill());
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            alive = false;
            animator.SetBool("Killed", true);

            StartCoroutine(WaitForKill());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.CompareTag("PlayerFeet"))
        {
            alive = false;
            animator.SetBool("Killed", true);

            StartCoroutine(WaitForKill());
        }
    }

    private IEnumerator WaitForKill()
    {
        yield return new WaitForSeconds(delayToKill);
        Destroy(this.gameObject);
    }
}
