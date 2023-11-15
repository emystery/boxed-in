using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    private float boxRespawnTime = 3.0f;
    private float currentBoxRespawnTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BoomBa")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Lava")
        {
            //Destroy(this.gameObject); // --hide box using renderer
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            Destroy(collision.gameObject);
        }
    }

    public void Respawn(Vector2 location)
    {
        transform.position = location;
        gameObject.SetActive(true);
        print("yo");
    }

    public void PickUp(Vector2 location)
    {
        transform.position = location;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void PlaceDown(Vector2 location)
    {
        transform.position = location;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
