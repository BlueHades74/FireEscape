using UnityEngine;

public class StaircaseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject exitStairs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TeleportPlayer(collision.gameObject);
        }
    }

    private void TeleportPlayer(GameObject playerToTP)
    {
        if (playerToTP != null)
        {
            string item = "";
            try
            {
                item = playerToTP.GetComponent<PlayerActionScript>().ReturnActionString();
            } 
            catch 
            {
                item = "null";
            }

            Vector2 tpPos = Vector2.zero;

            if (item == "Ladder")
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                for (int i = 0; i < players.Length; i++)
                {
                    tpPos = exitStairs.transform.position + (exitStairs.transform.up * (2*(i+1)));

                    if (players[0] == playerToTP)
                    {
                        players[((1 + i) % 2)].transform.position = tpPos;
                        
                    }
                    else
                    {
                        players[i % 2].transform.position = tpPos;
                    }
                }
            }
            else
            {
                tpPos = exitStairs.transform.position + (exitStairs.transform.up * 3);
                playerToTP.transform.position = tpPos;
            }

            Vector2 linearVelocityOP = playerToTP.GetComponent<Rigidbody2D>().linearVelocity;
            playerToTP.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
