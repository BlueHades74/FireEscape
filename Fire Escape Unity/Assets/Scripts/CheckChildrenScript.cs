using UnityEngine;

public class CheckChildrenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 2)
        {
            int objectcount = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).tag == "Object")
                {
                    objectcount++;
                }

                if (objectcount > 1)
                {
                    transform.GetChild(i).gameObject.GetComponent<ObjectManager>().DropItem();
                    objectcount--;
                }
            }
        }
    }
}
