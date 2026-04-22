using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited By: Logan Shade

    [SerializeField]
    private float modifier;

    [SerializeField]
    private GameObject player;

    private Image image;

    private GameObject transferObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeOpacity();
        if (modifier > 0 && image.color.a >= 1)
        {
            //If screen is fully dark
            modifier = -modifier;

            //Try and catch for Staircase vs Hole Jumping
            try 
            {
                transferObject.GetComponent<StaircaseScript>().TriggerTeleport(player);
            }
            catch
            {
                transferObject.GetComponent<HoleJumpScript>().TriggerTeleport();
            }

        }
        else if (modifier < 0 && image.color.a <= 0)
        {
            //If screen is fully transparent again
            modifier = -modifier;
            player.GetComponent<PlayerMovementScript>().CanMove(true);
            player.GetComponent<PlayerInteraction>().CanPickUp(true);
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Changes the opacity of the image by a modifier
    /// </summary>
    private void ChangeOpacity()
    {
        Color color = GetComponent<Image>().color;
        color.a += modifier;
        image.color = color;
    }

    /// <summary>
    /// Get the game object that is trying to move the player and immobilize the player
    /// </summary>
    /// <param name="sender"></param>
    public void ReceiveTransferObject(GameObject sender)
    {
        transferObject = sender;
        player.GetComponent<PlayerMovementScript>().CanMove(false);
        player.GetComponent<PlayerInteraction>().CanPickUp(false);
    }
}
