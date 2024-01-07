using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] wallsInactive;
    public GameObject[] ladders;

    public Tilemap startingWallsTilemap;
    public Tilemap changewallsTilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            ChangeWallMap();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            ResetWallMap();
        }
    }

    private void Start()
    {
        changewallsTilemap.gameObject.SetActive(false);
    }

    public void ChangeWallMap()
    {
        startingWallsTilemap.gameObject.SetActive(false);
        changewallsTilemap.gameObject.SetActive(true);
    }

    public void ResetWallMap()
    {
        startingWallsTilemap.gameObject.SetActive(true);
        changewallsTilemap.gameObject.SetActive(false);
    }

}
