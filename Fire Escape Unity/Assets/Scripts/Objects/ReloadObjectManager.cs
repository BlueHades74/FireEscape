using UnityEngine;

public class ReloadObjectManager : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private ObjectManager objectManager;
    private int delay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectManager = GetComponent<ObjectManager>();
        delay = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //After 2 frames run the methods
        if (delay > 1)
        {
            ResetObjectManager();
        }
        else
        {
            delay++;
        }
    }

    private void ResetObjectManager()
    {
        objectManager.enabled = false;
        objectManager.enabled = true;

        this.enabled = false;
    }
}
