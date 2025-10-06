// Author: Jacob Biles
/* Purpose & How to use
* Purpose: Gives interaction to the newspaper wall
* How to use:
* Attach to "Newspaper wall" (Interactable Level selection game object)
* Assign players to script
* Assign Canvas to levelSelectUI
* Get within range, determined via public variable: interactableDistace
* Press the use key to interact with the object to load the level select scene
*/

using UnityEngine;

public class PaperWall : MonoBehaviour
{
    // Players Variables
    public GameObject playerOne, playerTwo;

    // Desired distance variable
    public float interactableDistance;

    // Level select UI (canvas)
    public GameObject levelSelectUI;

    void Start()
    {
        // Start the level select UI off
        levelSelectUI.SetActive(false);
    }

    void Update()
    {
        // Gets and sets player distance from the object
        float playerOneDistance = Vector2.Distance(transform.position, playerOne.transform.position);
        float playerTwoDistance = Vector2.Distance(transform.position, playerTwo.transform.position);

        // If both players are in range....
        if (playerOneDistance <= interactableDistance || playerTwoDistance <= interactableDistance)
        {

            // If the E key is pressed.....
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Then open the level select UI
                levelSelectUI.SetActive(true);
            }

            // If the Escape key is pressed....
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Then close the level select UI
                levelSelectUI.SetActive(false);
            }
        }

        // If the players arent in range or get too far....
        else
        {
            // Then keep the level select UI closed
            levelSelectUI.SetActive(false);
        }

    }
    
}
