using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpForce, feetRadius, direction, fallingForce;
    public float xAxis, yAxis;
    public bool isGrounded;
    public bool climbing;
    public bool boxPlaced;
    Vector2 boxPosStored;
    Vector2 boxPosFloor;

    public float playerWidth, boxWidth;

    public Vector2 startPosition;
    public GameObject checkPoint;
    public Vector2 checkPointPos;
    public bool checkpoint;

    public bool obstacleOnTheRight;
    public bool obstableOnTheLeft;

    public LayerMask groundMask;
    public Transform characterFeet;
    public GameObject boxPrefab;

    private BoxScript box;

    private Rigidbody2D rb;
    private float boxRespawnTime = 3.0f;
    private float currentBoxRespawnTime = 0f;
    private float gravity = 4.0f;

    private SpriteRenderer spriteRenderer;

    private Animator anim;

    private const float delayToKill = 1.0f;

    private void Start()
    {
        climbing = false;
        rb = GetComponent<Rigidbody2D>();
        box = Instantiate(boxPrefab, new Vector2(rb.position.x + direction * -2, rb.position.y + 2), Quaternion.identity).GetComponent<BoxScript>();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
            checkPointPos = checkPoint.transform.position;
        }
        direction = 1;
        boxRespawnTime = Time.fixedTime + 3.0f;
        playerWidth = GetComponent<Renderer>().bounds.size.x;
        boxWidth = box.GetComponent<Renderer>().bounds.size.x;
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        boxPosFloor = new Vector2(rb.position.x + direction * 3, rb.position.y);

        if ((Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)) || ((Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)) && isGrounded))))
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isGrounded", true);

            if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
            {
                spriteRenderer.flipX = false;
            }
            else if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isGrounded", false);
        }

        if (xAxis != 0)
        {
            direction = xAxis;
        }

        isGrounded = Physics2D.Raycast(characterFeet.position, Vector2.down, feetRadius, groundMask);

        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W)) && isGrounded))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (climbing)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                rb.velocity = new Vector3(rb.velocity.x, fallingForce, 0);
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded && climbing == false)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            anim.Play("Jump");
        }

        if (!climbing && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            rb.velocity = new Vector3(rb.velocity.x, fallingForce, 0);
            anim.SetBool("isFalling", true);
        }

        float verticalVelocity = rb.velocity.y;

        if (verticalVelocity < 0)
        {
            anim.SetBool("isFalling", true);
        }
        else if (isGrounded)
        {
            anim.SetBool("isFalling", false);
        }

        if (climbing && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (boxPlaced == false)
        {
            boxPosStored = new Vector2(rb.position.x + direction * -2, rb.position.y + 2);
            box.transform.SetPositionAndRotation(boxPosStored, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Space) && boxPlaced == false)
        {
            if (direction < 0)
            {
                if (!Physics2D.Raycast(transform.position, Vector2.left, playerWidth * 2 + boxWidth, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(boxPosFloor);
                }
                else if (!Physics2D.Raycast(transform.position, Vector2.right, playerWidth * 2 + boxWidth, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(new Vector2(rb.position.x + direction * 3 + 2 * playerWidth + 2 * boxWidth, rb.position.y));
                }
            }
            else
            {
                if (!Physics2D.Raycast(transform.position, Vector2.right, playerWidth * 2 + boxWidth, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(boxPosFloor);
                }
                else if (!Physics2D.Raycast(transform.position, Vector2.left, playerWidth * 2 + boxWidth, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(new Vector2(rb.position.x + direction * 3 - 2 * playerWidth - 2 * boxWidth, rb.position.y));
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Space) && boxPlaced == true)
        {
            boxPlaced = false;
            box.PickUp(boxPosStored);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);

        if (!box.gameObject.activeSelf)
        {
            currentBoxRespawnTime += Time.fixedDeltaTime;
            print(currentBoxRespawnTime);

            if (currentBoxRespawnTime >= boxRespawnTime)
            {
                Vector2 respawnPosition = new Vector2(rb.position.x + direction * -2, rb.position.y + 2);

                box.Respawn(respawnPosition);
                boxPlaced = false;
                currentBoxRespawnTime = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
        }

        if (collision.gameObject.tag == "Slime")
        {
            Die();
        }
        if (collision.gameObject.tag == "BoomBa")
        {
            Die();
        }
        if (collision.gameObject.tag == "Doorman")
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BoomBa")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "ladderToDestroy" && !climbing)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
                rb.gravityScale = 0f;
                climbing = true;
            }
        }

        if (collision.gameObject.tag == "ladder" && !climbing)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
                rb.gravityScale = 0f;
                climbing = true;
            }
        }

        if (collision.gameObject.tag == "Lava")
        {
            Die();
        }
        if (collision.gameObject.tag == "Spikes")
        {
            Die();
        }

        if (collision.gameObject.tag == "CheckPoint")
        {
            checkpoint = true;
        }
        else
        {

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder" && !climbing)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
                rb.gravityScale = 0f;
                climbing = true;
            }
        }

        if (collision.gameObject.tag == "ladderToDestroy" && !climbing)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
                rb.gravityScale = 0f;
                climbing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("Player");
        rb.gravityScale = gravity;
        climbing = false;
    }

    void Die()
    {
        if (SceneManager.GetActiveScene().name  == "Game")
        {
            if (checkpoint)
            {
                transform.position = checkPointPos;
            }
            else
            {
                SceneManager.LoadScene("Game");
                //transform.position = startPosition;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}