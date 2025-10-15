// Author: Jacob Biles
/* Purpose & How to use
* Purpose: Gives interaction to the newspaper wall
* How to use:
* Attach to "Newspaper wall" (Interactable Level selection game object)
* Assign players to script
* Assign Canvas to levelSelectUI
* Assign to "On-Click" interaction for corresponding buttons.
* Get within range, determined via public variable: interactableDistace
* Press the use key to interact with the object to load the level select scene
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PaperWall : MonoBehaviour
{
    // Players Variables
    public GameObject playerOne, playerTwo;

    // Desired distance variable
    public float interactableDistance;

    // Interactable Highlight
    public Color highlightColor = Color.yellow;

    // Sprite Renderer component for the blackboard
    private SpriteRenderer blackboardSpriteRender;

    // Original Color
    private Color baseColor;

    // Level select UI (canvas)
    public GameObject levelSelectUI;

    void Start()
    {
        // Getting the sprite Renderer component
        blackboardSpriteRender = GetComponent<SpriteRenderer>();

        // Start the level select UI off
        levelSelectUI.SetActive(false);

        // Get the base color
        baseColor = blackboardSpriteRender.color;

    }

    void Update()
    {
        // Gets and sets player distance from the object
        float playerOneDistance = Vector2.Distance(transform.position, playerOne.transform.position);
        float playerTwoDistance = Vector2.Distance(transform.position, playerTwo.transform.position);

        // Activate level select check
        // Check to see if players are in range of blackboard
        if ((playerOneDistance <= interactableDistance || playerTwoDistance <= interactableDistance))
        {
            // Highlight the blackboard to hint at interactability
            blackboardSpriteRender.color = highlightColor;

            // Check to see if interact key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Activate (Open) the level select UI
                levelSelectUI.SetActive(true);
            }

        } // Activate level select end

        // Deactivate level select check
        // Check if the level select is already active
        else if (levelSelectUI.activeInHierarchy == true)
        {
            // Reset the blackboard to base color
            blackboardSpriteRender.color = baseColor;

            // Check is escape key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Disable (close) the level select UI
                levelSelectUI.SetActive(false);
            }

            // Check to see if the players exit the range of the blackboard
            else if (playerOneDistance > interactableDistance && playerTwoDistance > interactableDistance)
            {
                // Disable (close) the level select UI
                levelSelectUI.SetActive(false);
            }
        } // Deactive level select check end

        // Default check, 1) Players arent in range. 2) The ui isnt already active.
        // Disable the highlight
        else blackboardSpriteRender.color = baseColor; 
    }

    // Button Interaction to select a level.
    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name);
        Debug.Log("Loading: " + name);
    }

}
