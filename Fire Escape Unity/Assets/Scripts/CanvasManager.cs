using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> canvasList;
    [SerializeField]
    private PlayerMovementScript p1Move;
    [SerializeField]
    private PlayerMovementScript p2Move;

    private int count;

    [SerializeField]
    private bool active;

    public bool Active { get => active; set => active = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var canvas in canvasList)
        {
            if (canvas.gameObject.activeInHierarchy == true)
            {
                p1Move.CanMove(false);
                p2Move.CanMove(false);
            }
            else
            {
                count++;
            }
        }
            
        if (count == canvasList.Count && active == true)
        {
            p1Move.CanMove(true);
            p2Move.CanMove(true);
            active = false;
        }

        count = 0;
        
    }
}
