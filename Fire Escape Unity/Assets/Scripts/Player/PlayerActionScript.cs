using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionScript : MonoBehaviour
{
    private GameObject actionItem;
    private PlayerInputController inputs;

    [SerializeField]
    private Grid grid;

    private string action;

    [SerializeField]
    private GameObject waterRangePrefab;
    [SerializeField]
    private GameObject waterColliderPrefab;
    private GameObject waterRangeDisplay;

    [SerializeField]
    private GameObject extinguisherRangePrefab;
    [SerializeField]
    private GameObject extinguisherColliderPrefab;
    private GameObject extinguisherRangeDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(action)
        {
            case ("Bucket"):
                BucketHave();
                break;

            case ("Ladder"):
                LadderHave();
                break;

            case ("LadderHoleRemove"):
                RemoveLadder();
                break;

            case ("2PDebris"):
                Debris2PHave(); 
                break;

            case ("Extinguisher"):
                ExtinguisherHave();
                break;
        }
    }

    /// <summary>
    /// Subscribing.
    /// </summary>
    public void OnEnable()
    {
        inputs = GetComponent<PlayerInputController>();
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Action.performed += OnAction;
        }
        else
        {
            inputs.InputActions.Player.P2Action.performed += OnAction;
        }
    }

    /// <summary>
    /// Unsubscribing.
    /// </summary>
    private void OnDisable()
    {
        inputs = GetComponent<PlayerInputController>();
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Action.performed -= OnAction;
        }
        else
        {
            inputs.InputActions.Player.P2Action.performed -= OnAction;
        }
        actionItem = null;
        action = null;

        if (waterRangeDisplay != null)
        {
            Destroy(waterRangeDisplay);
            waterRangeDisplay = null;
        }

        if (extinguisherRangeDisplay != null)
        {
            Destroy(extinguisherRangeDisplay);
            extinguisherRangeDisplay = null;
        }
    }

    /// <summary>
    /// Executes an action.
    /// </summary>
    /// <param name="context"></param>
    private void OnAction(InputAction.CallbackContext context)
    {

        switch (action)
        {
            case ("Axe"):
                Debug.Log("SWING");
                AxeUse();
                break;

            case ("Bucket"):
                BucketUse(); 
                break;

            case ("Ladder"):
                LadderUse();
                break;

            case ("Extinguisher"):
                ExtinguisherUse();
                break;
        }
    }

    /// <summary>
    /// Receives and recognizes what action item they are holding
    /// </summary>
    /// <param name="actionItemSent"></param>
    public void ReceiveActionItem(GameObject actionItemSent)
    {
        actionItem = actionItemSent;

        action = actionItem.GetComponent<ObjectManager>().Action;

        switch (action)
        {
            case ("Bucket"):
                if (actionItem.GetComponent<WaterBucketScript>().IsFilled == true)
                {
                    waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab, transform.position, Quaternion.identity);
                }
                break;

            case ("Extinguisher"):
                extinguisherRangeDisplay = Instantiate<GameObject>(extinguisherRangePrefab, transform.position, Quaternion.identity);
                break;
        }
    }

    /// <summary>
    /// Slices with the axe
    /// </summary>
    private void AxeUse()
    {
        Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f);
        Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);

        Debug.Log(hit.collider);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "BreakableObject")
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Splashes water with the bucket and/or fills the bucket with water.
    /// </summary>
    private void BucketUse()
    {
        if (actionItem.GetComponent<WaterBucketScript>().IsFilled)
        {
            for (int i = 0; i < waterRangeDisplay.transform.childCount; i++)
            {
                var childLocation = waterRangeDisplay.transform.GetChild(i).transform.position;
                Instantiate<GameObject>(waterColliderPrefab, childLocation, Quaternion.identity);
            }
            actionItem.GetComponent<WaterBucketScript>().EmptyBucket();
            Destroy(waterRangeDisplay);
        }
        else
        {
            Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f);
            Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);

            Debug.Log(hit.collider);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "DropOff")
                {
                    actionItem.GetComponent<WaterBucketScript>().FillBucket();
                    waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// Places the Ladder Down.
    /// </summary>
    private void LadderUse()
    {
        Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        ContactFilter2D contactFilter = new ContactFilter2D();
        RaycastHit2D[] hits = new RaycastHit2D[10];
        Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, contactFilter, hits, 1.5f);
        Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Hole")
                {
                    //hit.collider.gameObject.GetComponentInChildren<LadderCoverPit>().ReceiveLadder(actionItem.GetComponent<LadderPickupLogic>().Ladder);
                    hit.collider.gameObject.GetComponent<LadderCoverPit>().ReceiveLadder(actionItem.GetComponent<LadderPickupLogic>().Ladder);
                }
            }
        }
    }

    private void ExtinguisherUse()
    {
        Vector3 childLocation = extinguisherRangeDisplay.transform.GetChild(0).transform.position;
        Instantiate<GameObject>(extinguisherColliderPrefab, childLocation, Quaternion.identity);
    }

    /// <summary>
    /// For when the player has the bucket, does the grid thing.
    /// </summary>
    private void BucketHave()
    {
        if (actionItem.GetComponent<WaterBucketScript>().IsFilled)
        {
            Vector3 facingDisplace = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
            Vector3Int waterSpawnLocation = grid.WorldToCell(transform.position + facingDisplace);

            waterRangeDisplay.transform.position = grid.CellToWorld(waterSpawnLocation);
        }
    }

    /// <summary>
    /// For when the player has the ladder in hand, sets its position and the player's movement speed.
    /// </summary>
    private void LadderHave()
    {
        GameObject ladder = actionItem.GetComponent<LadderPickupLogic>().Ladder;

        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(transform.position.x, ladder.transform.position.x - 1.3f, ladder.transform.position.x + 1.3f);
        position.y = Mathf.Clamp(transform.position.y, ladder.transform.position.y - 1.3f, ladder.transform.position.y + 1.3f);

        transform.position = position;

        if (ladder.GetComponent<LadderScript>().IsFullyPickedUp == true)
        {
            GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0.4f);
        }
        else
        {
            GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0);
        }
    }

    /// <summary>
    /// When the player is trying to remove the ladder from the hole.
    /// </summary>
    private void RemoveLadder()
    {
        GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0);
    }

    private void Debris2PHave()
    {
        GameObject debris = actionItem.GetComponent<DebrisPickup>().OriginalParent;

        if (debris.GetComponent<DebrisScript>().IsCarriedByTwoPlayers == false)
        {
            GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0);
        }
        else
        {
            GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0.7f);
        }

        Vector3 position = Vector3.zero;

        if ((debris.transform.rotation.z < 0.1 && debris.transform.rotation.z > -0.1))
        {
            if (debris.transform.position.x > transform.position.x)
            {
                position.x = Mathf.Clamp(transform.position.x, debris.transform.position.x - 1.9f, debris.transform.position.x - 1.8f);
            }
            else
            {
                position.x = Mathf.Clamp(transform.position.x, debris.transform.position.x + 1.8f, debris.transform.position.x + 1.9f);
            }

            position.y = Mathf.Clamp(transform.position.y, debris.transform.position.y - 1, debris.transform.position.y + 1);
        }
        else
        {
            if (debris.transform.position.y > transform.position.y)
            {
                position.y = Mathf.Clamp(transform.position.y, debris.transform.position.y - 1.9f, debris.transform.position.y - 1.8f);
            }
            else
            {
                position.y = Mathf.Clamp(transform.position.y, debris.transform.position.y + 1.8f, debris.transform.position.y + 1.9f);
            }

            position.x = Mathf.Clamp(transform.position.x, debris.transform.position.x - 1, debris.transform.position.x + 1);
        }    

        transform.position = position;
    }

    private void ExtinguisherHave()
    {
        Vector3 facingDisplace = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        Vector3Int extinguisherSpawnLocation = grid.WorldToCell(transform.position + facingDisplace);

        extinguisherRangeDisplay.transform.position = grid.CellToWorld(extinguisherSpawnLocation);
    }
}