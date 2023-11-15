using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public float speed, jumpForce, feetRadius, direction;

    public float xAxis, yAxis;

    public bool isGrounded;

    public bool boxPlaced;



    public LayerMask groundMask;

    public Transform characterFeet;

    public GameObject box;

    private Rigidbody2D rb;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = Instantiate(box, new Vector2(rb.position.x + direction * -2, rb.position.y + 2), Quaternion.identity);
        direction = 1;

    }

    // Update is called once per frame

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        
        if (xAxis != 0)
        {
            direction = xAxis;
        }


        isGrounded = Physics2D.Raycast(characterFeet.position, Vector3.down, feetRadius, groundMask);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        if (boxPlaced == false)
        {
            box.transform.SetPositionAndRotation(new Vector2(rb.position.x + direction * -2, rb.position.y + 2), Quaternion.identity);
           
        }

        if (Input.GetKeyDown(KeyCode.Space) && boxPlaced == false)
        {
            boxPlaced = true;
            box.transform.SetPositionAndRotation(new Vector2(rb.position.x + direction * 3, rb.position.y), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && boxPlaced == true)
        {
            boxPlaced = false;
            //box.transform.SetPositionAndRotation(new Vector2(rb.position.x + xAxis * 2, rb.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
        }



    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(xAxis * speed, rb.velocity.y, 0);

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = new Vector3(0,0,0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision stay with " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision exit with " + collision.gameObject.name);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Slime")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "BoomBa")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}

