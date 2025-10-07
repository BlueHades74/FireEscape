using UnityEngine;

public class DropOff : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //When this object touchs the object with tag "DropOff" Destory this object
        if (collision.CompareTag("DropOff"))
        {
            //Calls the public void within ObjectManager to destroy object and alter the objective count.
            GetComponent<ObjectManager>()?.Rescue();
        }
    }
}
