using System.Collections;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private bool isMovingRight = true;
    public LayerMask wallLayer;

    private bool isCollidingWithBox = false;
    private bool isCollidingWithWall = false;

    private SpriteRenderer spriteRenderer;

    public Animator animator;

    private const float delayToKill = 2.0f;
    public bool alive;

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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 0.1f, wallLayer);

            if (hit.collider != null)
            {
                isMovingRight = !isMovingRight;
            }

            if (isMovingRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                spriteRenderer.flipX = true;
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                spriteRenderer.flipX = false;
            }

            if (isCollidingWithBox && isCollidingWithWall)
            {
                alive = false;
                animator.SetBool("Killed", true);
                StartCoroutine(WaitForKill());
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isCollidingWithBox = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollidingWithWall = true;
        }

        if (collision.gameObject.CompareTag("Slime"))
        {
            isMovingRight = !isMovingRight;
        }

        if (collision.gameObject.CompareTag("BoomBa"))
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isCollidingWithBox = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollidingWithWall = false;
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

