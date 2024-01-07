using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlateEasterEgg : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] wallsInactive;
    public GameObject[] ladders;

    public Tilemap eastereggwallsTilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            ChangeWallMapEasterEgg();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            ResetWallMapEasterEgg();
        }
    }

    public void ChangeWallMapEasterEgg()
    {
        eastereggwallsTilemap.gameObject.SetActive(false);
    }

    public void ResetWallMapEasterEgg()
    {
        eastereggwallsTilemap.gameObject.SetActive(true);
    }

}
