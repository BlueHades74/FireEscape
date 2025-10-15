using UnityEngine;

public class UnstuckScript : MonoBehaviour
{
    private GameObject[] players;
    private LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        layerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1f, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag != "Player" & hit.collider.gameObject.name != gameObject.name & !hit.collider.isTrigger)
            {
                GameObject nearestPlayer = GetNearestPlayer();
                transform.position = nearestPlayer.transform.position;
            }
        }
    }

    private GameObject GetNearestPlayer()
    {
        float distanceP1 = Mathf.Sqrt(Mathf.Pow(players[0].transform.position.x, 2) + Mathf.Pow(players[0].transform.position.y, 2));
        float distanceP2 = Mathf.Sqrt(Mathf.Pow(players[1].transform.position.x, 2) + Mathf.Pow(players[1].transform.position.y, 2));

        if (distanceP1 > distanceP2)
        {
            return players[1];
        }
        else
        {
            return players[0];
        }
    }
}
