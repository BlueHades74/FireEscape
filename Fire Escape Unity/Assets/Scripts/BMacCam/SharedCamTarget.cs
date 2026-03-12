using UnityEngine;

public class SharedCamTarget : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    void LateUpdate()
    {
        // create a vector3 to store difference of player positions
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        // modify the Z value to keep the camera back
        midpoint.z = transform.position.z;
        // set camera position
        transform.position = midpoint;
    }
}
