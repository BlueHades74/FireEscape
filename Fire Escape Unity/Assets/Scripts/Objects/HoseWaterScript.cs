using UnityEngine;

public class HoseWaterScript : MonoBehaviour
{
    //Created by: Rafael
    //Last Edited by: Rafael

    private HoseNozzleScript hoseScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoseScript = transform.parent.transform.parent.GetComponent<HoseNozzleScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Destroy the other thing if it is fire
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
