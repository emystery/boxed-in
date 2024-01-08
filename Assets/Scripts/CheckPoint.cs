using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public Tilemap CheckPointTileMap;

    void Start()
    {
        // Initialize the Animator component
        anim = GetComponent<Animator>();
        CheckPointTileMap.gameObject.SetActive(true);
    }

    public void ChangeWallMap()
    {
        CheckPointTileMap.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional update logic here if needed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision made");

            // Check if the Animator component is not null before playing the animation
            if (anim != null)
            {
                anim.Play("Check");
                ChangeWallMap();
            }
            else
            {
                Debug.LogError("Animator component is not initialized!");
            }
        }
    }
}

