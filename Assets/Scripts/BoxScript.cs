using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    public GameObject wallToDestroy;
    public GameObject wallToDestroyInactive;
    public GameObject easterEgg;
    public GameObject ladder;
    GameObject[] walls;
    GameObject[] wallsInactive;
    GameObject[] ladders;
    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("WallToDestroy");
        wallsInactive = GameObject.FindGameObjectsWithTag("WallToDestroyInactive");
        easterEgg = GameObject.FindGameObjectWithTag("WallToDestroyEasterEgg");
        ladders = GameObject.FindGameObjectsWithTag("ladder");

        ActivateWalls();
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
        else if (collision.gameObject.tag == "PressurePlateEasterEgg")
        {
            easterEgg.SetActive(false);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PressurePlate")
        {

            ActivateWalls();
        }
        else if (collision.gameObject.tag == "PressurePlateEasterEgg")
        {

            easterEgg.SetActive(true);
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
            
        }

        foreach (GameObject ladder in ladders)
        {
            ladder.SetActive(true);

        }

        if (ladders != null)
        {
            ladder.SetActive(false);
        }
        else
        {

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
            
        }

        foreach (GameObject ladder in ladders)
        {
            ladder.SetActive(false);
        }

        if (ladder != null)
        {
            ladder.SetActive(true);
        }
        else
        {

        }
    }
}
