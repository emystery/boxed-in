using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpForce, feetRadius, direction;
    public float xAxis, yAxis;
    public bool isGrounded;
    public bool boxPlaced;
    Vector2 boxPosStored;
    Vector2 boxPosFloor;
    public LayerMask groundMask;
    public Transform characterFeet;
    public GameObject boxPrefab;

    private BoxScript box;

    private Rigidbody2D rb;
    private float boxRespawnTime = 3.0f;
    private float currentBoxRespawnTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = Instantiate(boxPrefab, new Vector2(rb.position.x + direction * -2, rb.position.y + 2), Quaternion.identity).GetComponent<BoxScript>();
        direction = 1;
        boxRespawnTime = Time.fixedTime + 3.0f;
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        boxPosFloor = new Vector2(rb.position.x + direction * 3, rb.position.y);


        if (xAxis != 0)
        {
            direction = xAxis;
        }

        isGrounded = Physics2D.Raycast(characterFeet.position, Vector3.down, feetRadius, groundMask);


        if (boxPlaced == false)
        {
            boxPosStored = new Vector2(rb.position.x + direction * -2, rb.position.y + 2);
            box.transform.SetPositionAndRotation(boxPosStored, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && boxPlaced == false)
        {
            boxPlaced = true;
            box.PlaceDown(boxPosFloor);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && boxPlaced == true)
        {
            boxPlaced = false;
            box.PickUp(boxPosStored);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(xAxis * speed, rb.velocity.y, 0);

        if (!box.gameObject.active)
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
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "BoomBa")
        {
            Destroy(collision.gameObject);
        }
    }
}