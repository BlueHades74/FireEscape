// Made by Jacob Biles
// How to use (Non-Prefab):
/* 
* 1. Create empty GameObject
* 2. Attach a Sprite render script
* 3. Attach this script
*   3a. Attach a sprite
    3b. attach a quest item object (Prefab)
    3c. Assign a turn in range.
    3b. Assign the name to the npc (For dialog)
*/
// How To use (Prefab):
/*
* 1. Attach a sprite
  2. attach a quest item object (Prefab)
  3. Assign a turn in range.
  4. Assign the name to the npc (For dialog)
*/

using UnityEngine;

public class QuestNPC : NPCBase
{
    // The item to be turned in
    public GameObject questTarget;
    // The range in which it can be turned in
    public float turnInRange;
    // The players
    private GameObject playerOne, playerTwo;
    private bool hasHadInitialDialogTrigger = false;
    public bool HasHadInitialDialogTrigger { get => hasHadInitialDialogTrigger; set => hasHadInitialDialogTrigger = value; }
    private bool hasHadPickupDialogTrigger = false;
    public bool HasHadPickupDialogTrigger { get => hasHadPickupDialogTrigger; set => hasHadPickupDialogTrigger = value; }

    void Start()
    {
         foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.name == "Player 1") playerOne = p;
            else if (p.name == "Player 2") playerTwo = p;
        }
        turnInRange = interactableDistace;
    }

    void Update()
    {

        // Ensure the quest Target exists
        if (questTarget != null)
        {
            // Calculate how far the item is to this npc
            float distanceToTarget = Vector2.Distance(transform.position, questTarget.transform.position);
            // If within range
            if (distanceToTarget < turnInRange)
            {
                // Actions to be taken (Trigger dialog in the future?)
                Debug.Log("Yay you did it");
                // Destroy the object
                Destroy(questTarget);
            }
        }

    }

    public (int, bool) isPlayerWithinRange()
    {
        float playerOneDistance = Vector2.Distance(transform.position, playerOne.transform.position);
        float playerTwoDistance = Vector2.Distance(transform.position, playerTwo.transform.position);
        if (playerOneDistance < interactableDistace) return (1, true);
        else if (playerTwoDistance < interactableDistace) return (2, true);
        return (0, false);

    }
    
}

