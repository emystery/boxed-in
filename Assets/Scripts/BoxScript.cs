using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{

    public GameObject wallToDestroy;
    public GameObject wallToDestroyInactive;
    GameObject[] walls;
    GameObject[] wallsInactive;

    public Sprite boxDefault;
    public Sprite nelioBox;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("WallToDestroy");
        wallsInactive = GameObject.FindGameObjectsWithTag("WallToDestroyInactive");

        ActivateWalls();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            GetComponent<SpriteRenderer>().sprite = boxDefault;
        }
        else if (Input.GetKeyDown("2"))
        {
            GetComponent<SpriteRenderer>().sprite = nelioBox;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BoomBa")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Lava")
        {
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "PressurePlate")
        {
            walls = collision.gameObject.GetComponent<PressurePlate>().walls;
            DeactivateWalls();

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PressurePlate")
        {

            ActivateWalls();
        }
    }

    public void Respawn(Vector2 location)
    {
        transform.position = location;
        gameObject.SetActive(true);
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

    private void DeactivateWalls()
    {

        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);

        }

        if (wallToDestroy != null)
        {
            wallToDestroy.SetActive(true);
        }
        else
        {
            Debug.LogWarning("WallToDestroy not found. Make sure the wall has the correct tag.");
        }

        foreach (GameObject wallInactive in wallsInactive)
        {
            wallInactive.SetActive(true);

        }

        if (wallToDestroyInactive != null)
        {
            wallToDestroyInactive.SetActive(false);
        }
        else
        {
            Debug.LogWarning("WallToDestroyInactive not found. Make sure the wall has the correct tag.");
        }
    }

    private void ActivateWalls()
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(true);

        }

        if (wallToDestroy != null)
        {
            wallToDestroy.SetActive(false);
        }
        else
        {
            Debug.LogWarning("WallToDestroy not found. Make sure the wall has the correct tag.");
        }

        foreach (GameObject wallInactive in wallsInactive)
        {
            wallInactive.SetActive(false);
        }

        if (wallToDestroyInactive != null)
        {
            wallToDestroyInactive.SetActive(true);
        }
        else
        {
            Debug.LogWarning("WallToDestroyInactive not found. Make sure the wall has the correct tag.");
        }
    }
}
