using UnityEngine;

public class DoormanScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoxFeet"))
        {
            Destroy(this.gameObject);
        }
    }
}
