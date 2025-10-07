using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelInputManager : MonoBehaviour
{
    //Created By: Brandon Stih
    //Last Updated: 9/22/2025 By Brandon Stih

    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetSelectedMapPosition()
    {
        //Handles Conversion of Mouse coordinates into a usable Vector3
        //that correlates to somewhere on the canvas.

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
            
        }
        return lastPosition;
    }
}
