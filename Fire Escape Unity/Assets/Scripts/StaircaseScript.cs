using System.Collections;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class StaircaseScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    [SerializeField]
    private GameObject exitStairs;

    private GameObject playerToTP;

    [SerializeField]
    private GameObject[] transitionScreens;
    private RectTransform blackScreenRectLeft, blackScreenRectRight;

    [SerializeField] private GameObject vDualCamLeft;
    [SerializeField] private GameObject vDualCamRight;

    public delegate void CanMidpoint();
    public static event CanMidpoint canMidpointFlip;

    private void OnEnable()
    {
        SharedCamTarget.swapBlackScreens += SwapBlackScreenSides;
    }

    private void OnDisable()
    {
        SharedCamTarget.swapBlackScreens -= SwapBlackScreenSides;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blackScreenRectLeft = transitionScreens[0].GetComponent<RectTransform>();
        blackScreenRectRight = transitionScreens[1].GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Detect when the player enters the stairs
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            vDualCamLeft.SetActive(false);
            vDualCamRight.SetActive(false);
            playerToTP = collision.gameObject;
            BeginTransition();
        }
    }

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
        { 
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            FloorManager.Instance.GoUpFloor();
        }
    }

    private void SwapBlackScreenSides(bool notSwapped)
    {
        if (notSwapped == true)
        {
            blackScreenRectLeft.anchoredPosition = new Vector2(0f, 0);
            blackScreenRectRight.anchoredPosition = new Vector2(0f, 0);
        }
        else if (notSwapped == false)
        {
            blackScreenRectLeft.anchoredPosition = new Vector2(960f, 0);
            blackScreenRectRight.anchoredPosition = new Vector2(-960f, 0);
        }
    }

    /// <summary>
    /// Teleports the player to a location (changes how the teleport works depending on what the player is holding)
    /// </summary>
    /// <param name="playerToTP"></param>
    private void TeleportPlayer()
    {
        if (playerToTP != null)
        {
            //Detect what the player is holding
            string item = "";
            try
            {
                item = playerToTP.GetComponent<PlayerActionScript>().ReturnActionString();
            } 
            catch 
            {
                item = "null";
            }

            //Teleport based on item
            Vector2 tpPos = Vector2.zero;

            if (item == "Ladder")
            {
                //Get both players and teleport them to different locations based on who's entering
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
                //Teleport the entering player
                tpPos = exitStairs.transform.position + (exitStairs.transform.up * 3);
                playerToTP.transform.position = tpPos;
            }

            //Reset the linear velocity to make it seem more smooth
            playerToTP.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Begin the screen fade out and fade in
    /// </summary>
    private void BeginTransition()
    {
        if (playerToTP != null)
        {

            //Detect what the player is holding
            string item = "";
            try
            {
                item = playerToTP.GetComponent<PlayerActionScript>().ReturnActionString();
            }
            catch
            {
                item = "null";
            }

            if (item == "Ladder")
            {
                transitionScreens[0].SetActive(true);
                transitionScreens[0].SetActive(true);
                transitionScreens[0].GetComponent<ScreenFadeScript>().ReceiveTransferObject(this.gameObject);
                transitionScreens[1].SetActive(true);
                transitionScreens[1].GetComponent<ScreenFadeScript>().ReceiveTransferObject(this.gameObject);

                transitionScreens[1].SetActive(true);
            }
            else
            {
                //view.GetComponent<CameraTransition>().SetDistanceToValue(0);
                if (playerToTP.name == "Player 1")
                {
                    transitionScreens[0].SetActive(true);
                    transitionScreens[0].GetComponent<ScreenFadeScript>().ReceiveTransferObject(this.gameObject);
                }
                else
                {
                    transitionScreens[1].SetActive(true);
                    transitionScreens[1].GetComponent<ScreenFadeScript>().ReceiveTransferObject(this.gameObject);
                }
            }

            //Reset the linear velocity to make it seem more smooth
            playerToTP.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            
        }
    }

    /// <summary>
    /// This allows other objects to trigger the teleport
    /// </summary>
    public void TriggerTeleport(GameObject player)
    {
        playerToTP = player;
        canMidpointFlip?.Invoke();
        TeleportPlayer();
    }
}
