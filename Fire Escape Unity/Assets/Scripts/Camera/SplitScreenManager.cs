using UnityEngine;
using Unity.Cinemachine;

public class SplitScreenManager : MonoBehaviour
{
     
    public Transform player1;
    public Transform player2;
    
    public Camera leftCam;
    public Camera rightCam;
    public CinemachineCamera vcamShared;
    public CinemachineCamera vcamPlayer1;
    public CinemachineCamera vcamPlayer2;

    public float splitDistance = 12f;  // When to start splitting
    public float mergeDistance = 8f;   // When to merge back
    public float smoothSpeed = 5f;     // How smooth the transitions are

    private bool isSplit = false;

    void Update()
    {
        float distance = Vector2.Distance(player1.position, player2.position);

        if (!isSplit && distance > splitDistance)
            isSplit = true;
        else if (isSplit && distance < mergeDistance)
            isSplit = false;

        // Smoothly changes viewport widths
        float targetWidth = isSplit ? 0.5f : 1f;
        float newWidth = Mathf.Lerp(leftCam.rect.width, targetWidth, Time.deltaTime * smoothSpeed);

        // Apply rects
        if (isSplit)
        {
            leftCam.rect = new Rect(0f, 0f, newWidth, 1f);
            rightCam.rect = new Rect(1f - newWidth, 0f, newWidth, 1f);
            rightCam.enabled = true;

            // Boost player cams
            vcamShared.Priority = 10;
            vcamPlayer1.Priority = 20;
            vcamPlayer2.Priority = 20;
        }
        else
        {
            leftCam.rect = new Rect(0f, 0f, 1f, 1f);
            rightCam.enabled = false;

            // Boost shared cam
            vcamShared.Priority = 20;
            vcamPlayer1.Priority = 10;
            vcamPlayer2.Priority = 10;
        }
    }
}
