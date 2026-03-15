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

    // Level select UI (canvas)
    public GameObject levelSelectUI;

    //Canvas Manager (To detect when the canvas is open
    [SerializeField]
    private CanvasManager canvasManager;

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

        // If the E key is pressed.....
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Submit")) && (playerOneDistance <= interactableDistance || playerTwoDistance <= interactableDistance) && !canvasManager.Active)
        {
            // Then open the level select UI
            levelSelectUI.SetActive(true);
            GetComponent<ControllerUI>().MoveToElement();
            canvasManager.Active = true;
        }

        // If the Escape key is pressed....
        else if (levelSelectUI.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
            {
                // Then close the level select UI
                levelSelectUI.SetActive(false);
            }

            else if (playerOneDistance > interactableDistance && playerTwoDistance > interactableDistance)
            {
                // Then close the level select UI
                levelSelectUI.SetActive(false);
            }
        }
    }

    // Button Interaction to select a level.
    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name);
        Debug.Log("Loading: " + name);
    }

}
