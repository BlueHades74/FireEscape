using UnityEngine;

public class WaterColliderScript : MonoBehaviour
{
    private int timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer++;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
