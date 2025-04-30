using UnityEngine;

public class DropOff : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //When this object touchs the object with tag "DropOff" Destory this object
        if (collision.CompareTag("DropOff"))
        {
            Destroy(gameObject);
        }
    }
}
