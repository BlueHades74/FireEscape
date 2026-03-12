using UnityEngine;

public class DynamicSplitScreen : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;

    [SerializeField] private float splitDistance = 10f;
    [SerializeField] private float cameraSmooth = 5f;

    [SerializeField] private GameObject splitter;

    Vector3 camOffset = new Vector3(0, 0, -10);

    Vector3 cam1Velocity;
    Vector3 cam2Velocity;

    Vector3 target1;
    Vector3 target2;

    void LateUpdate()
    {
        float distance = Vector2.Distance(player1.position, player2.position);

        Vector2 midpoint = (player1.position + player2.position) / 2;

        if (distance < splitDistance)
        {
            // single cam

            cam2.enabled = false;
            splitter.SetActive(false);

            cam1.rect = new Rect(0, 0, 1, 1);

            Vector3 targetPos = new Vector3(midpoint.x, midpoint.y, -10);

            cam1.transform.position = Vector3.SmoothDamp(
                cam1.transform.position,
                targetPos,
                ref cam1Velocity,
                0.2f
            );
        }
        else
        {
            // split screen

            cam2.enabled = true;
            splitter.SetActive(true);

            cam1.rect = new Rect(0, 0, 0.5f, 1);
            cam2.rect = new Rect(0.5f, 0, 0.5f, 1);

            // add swapping sides here
            if (player1.position.x < player2.position.x && player1.position.y < splitDistance && player2.position.y < splitDistance)
            {
                target1 = player1.position + camOffset;
                target2 = player2.position + camOffset;
            }
            else
            {
                target2 = player1.position + camOffset;
                target1 = player2.position + camOffset;
            }

            cam1.transform.position = Vector3.SmoothDamp(
                cam1.transform.position,
                target1,
                ref cam1Velocity,
                cameraSmooth
            );

            cam2.transform.position = Vector3.SmoothDamp(
                cam2.transform.position,
                target2,
                ref cam2Velocity,
                cameraSmooth
            );

            Vector2 dir = player2.position - player1.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            splitter.transform.position = midpoint;
            splitter.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
