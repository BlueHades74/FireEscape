using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [Header("Player References")]
    public Transform player1;
    public Transform player2;

    [Header("Cameras")]
    public Camera sharedCamera;
    public Camera player1Camera;
    public Camera player2Camera;

    [Header("Split Distance")]
    public float splitDistance = 10f;

    [Header("Shared Camera Follow Speed")]
    public float followSpeed = 5f;

    void Start()
    {
        // Start with shared camera enabled
        sharedCamera.enabled = true;
        player1Camera.enabled = false;
        player2Camera.enabled = false;

        // Set split screen viewports
        player1Camera.rect = new Rect(0, 0, 0.5f, 1);
        player2Camera.rect = new Rect(0.5f, 0, 0.5f, 1);
    }

    void Update()
    {
        float distance = Vector3.Distance(player1.position, player2.position);

        if (distance < splitDistance)
        {
            // Use shared camera
            if (!sharedCamera.enabled)
            {
                sharedCamera.enabled = true;
                player1Camera.enabled = false;
                player2Camera.enabled = false;
            }

            // Smooth follow midpoint
            Vector3 midpoint = (player1.position + player2.position) / 2f;
            Vector3 targetPosition = new Vector3(midpoint.x, midpoint.y, sharedCamera.transform.position.z);
            sharedCamera.transform.position = Vector3.Lerp(sharedCamera.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            // Use split screen cameras
            if (!player1Camera.enabled)
            {
                sharedCamera.enabled = false;
                player1Camera.enabled = true;
                player2Camera.enabled = true;
            }

            // Optionally, have each camera follow its player (you can set this up via separate scripts or here)
            player1Camera.transform.position = new Vector3(player1.position.x, player1.position.y, player1Camera.transform.position.z);
            player2Camera.transform.position = new Vector3(player2.position.x, player2.position.y, player2Camera.transform.position.z);
        }
    }

    
    public void SetDistanceToValue(int value)
    {
        splitDistance = value;
    }
}

