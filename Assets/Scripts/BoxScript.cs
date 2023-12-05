using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{

    public GameObject wallToDestroy;
    public GameObject wallToDestroyInactive;
    public GameObject ladder;
    public GameObject easterEgg;
    GameObject[] walls;
    GameObject[] wallsInactive;
    GameObject[] ladders;
    GameObject[] easterEggs;

    public Sprite boxDefault;
    public Sprite nelioBox;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("WallToDestroy");
        wallsInactive = GameObject.FindGameObjectsWithTag("WallToDestroyInactive");
        ladders = GameObject.FindGameObjectsWithTag("ladderToDestroy");
        easterEggs = GameObject.FindGameObjectsWithTag("WallToDestroyEasterEgg");

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
        else if (collision.gameObject.tag == "PressurePlateEasterEgg")
        {
            DeactivateWallsEasterEgg();
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
            ActivateWallsEasterEgg();
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
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void PlaceDown(Vector2 location)
    {
        transform.position = location;
        gameObject.GetComponent<Collider2D>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void DeactivateWallsEasterEgg()
    {
        foreach (GameObject easterEgg in easterEggs)
        {
            easterEgg.SetActive(false);

        }

        if (easterEgg != null)
        {
            easterEgg.SetActive(true);
        }
        else
        {
        }
    }

    private void ActivateWallsEasterEgg()
    {
        foreach (GameObject easterEgg in easterEggs)
        {
            easterEgg.SetActive(true);

        }

        if (easterEgg != null)
        {
            easterEgg.SetActive(false);
        }
        else
        {
        }
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

        if (ladder != null)
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
