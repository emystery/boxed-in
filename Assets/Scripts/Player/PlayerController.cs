using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpForce, feetRadius, direction, fallingForce;
    public float xAxis, yAxis;
    public bool isGrounded;
    public bool climbing;
    public bool boxPlaced;
    Vector2 boxPosStored;
    Vector2 boxPosFloor;
    public float boxOffset;

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

    public BoxScript box;

    private Rigidbody2D rb;
    public float boxRespawnTime = 3.0f;
    public float currentBoxRespawnTime = 0f;
    private float gravity = 4.0f;

    private SpriteRenderer spriteRenderer;

    private Animator anim;

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
        boxPosFloor = new Vector2(rb.position.x + direction * 3, rb.position.y);

        if (!box.gameObject.activeSelf)
        {
            currentBoxRespawnTime += Time.deltaTime;
            print(currentBoxRespawnTime);

            if (currentBoxRespawnTime > boxRespawnTime)
            {
                Vector2 respawnPosition = new Vector2(rb.position.x + direction * -2, rb.position.y + 2);
                box.gameObject.SetActive(true);

                box.Respawn(respawnPosition);
                boxPlaced = false;
                currentBoxRespawnTime = 0;
            }
        }

        FlipAnim();
        MovementController();
        BoxController();
        
    }

    private void MovementController()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        isGrounded = Physics2D.Raycast(characterFeet.position, Vector2.down, feetRadius, groundMask);

        if (xAxis != 0)
        {
            direction = xAxis;
        }

        if (xAxis != 0 && isGrounded)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isGrounded", false);
        }

        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        if (climbing)
        {
            if (yAxis > 0)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            else if (yAxis < 0)
                rb.velocity = new Vector2(rb.velocity.x, fallingForce);
        }

        if (yAxis > 0 && isGrounded && !climbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.Play("Jump");
        }

        if (yAxis < 0 && !climbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, fallingForce);
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

        if (climbing && yAxis == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void FlipAnim()
    {
        if (xAxis > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (xAxis < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    
    private void BoxController()
    {
        boxOffset = playerWidth * 1.5f + boxWidth;

        if (!boxPlaced)
        {
            boxPosStored = new Vector2(rb.position.x + direction * -2, rb.position.y + 2);
            box.transform.SetPositionAndRotation(boxPosStored, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !boxPlaced)
        {
            if (direction < 0)
            {
                if (!Physics2D.Raycast(transform.position, Vector2.left,boxOffset, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(boxPosFloor);
                }
                else if (!Physics2D.Raycast(transform.position, Vector2.right, boxOffset, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(new Vector2(rb.position.x + direction * 3 + 2 * playerWidth + 2 * boxWidth, rb.position.y));
                }
            }
            else
            {
                if (!Physics2D.Raycast(transform.position, Vector2.right, boxOffset, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(boxPosFloor);
                }
                else if (!Physics2D.Raycast(transform.position, Vector2.left, boxOffset, groundMask))
                {
                    boxPlaced = true;
                    box.PlaceDown(new Vector2(rb.position.x + direction * 3 - 2 * playerWidth - 2 * boxWidth, rb.position.y));
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Space) && boxPlaced)
        {
            boxPlaced = false;
            box.PickUp(boxPosStored);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
        }

        if (collision.gameObject.CompareTag("Slime"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("BoomBa"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Doorman"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ladderToDestroy") && !climbing)
        {
            if (yAxis != 0)
            {
                gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
                rb.gravityScale = 0f;
                climbing = true;
            }
        }

        if (collision.gameObject.CompareTag("ladder") && !climbing)
        {
            if (yAxis != 0)
            {
                gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
                rb.gravityScale = 0f;
                climbing = true;
            }
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkpoint = true;
        }
        else
        {

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ladder") && !climbing)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                gameObject.layer = characterFeet.gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
                rb.gravityScale = 0f;
                climbing = true;
            }
        }

        if (collision.gameObject.CompareTag("ladderToDestroy") && !climbing)
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
        if (SceneManager.GetActiveScene().name == "Game")
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