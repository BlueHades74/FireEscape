using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActionScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles
    //Audio Relate Hoohah added by henry cummings
    [SerializeField]
    private AudioSource soundEffectSource;

    private GameObject actionItem;
    private PlayerInputController inputs;

    [SerializeField]
    private Grid grid;

    private string action;

    private bool holdCheck;

    [SerializeField]
    private AudioClip axeChopSound;

    [SerializeField]
    private GameObject waterRangePrefab;
    [SerializeField]
    private GameObject waterColliderPrefab;
    private GameObject waterRangeDisplay;
    [SerializeField]
    private AudioClip waterBucketSound;

    private float crowbarTimer;
    private Image crowbarFillBar;
    [SerializeField]
    private AudioClip crowbarPrySound;

    [SerializeField]
    private GameObject extinguisherRangePrefab;
    [SerializeField]
    private GameObject extinguisherColliderPrefab;
    private GameObject extinguisherRangeDisplay;
    [SerializeField]
    private AudioClip extinguisherSpraySound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crowbarFillBar = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (action)
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

            case ("1PDebris"):
                Debris1PHave();
                break;

            // Added by: Jacob Biles to cover default/unimplemented items
            default:
                //Debug.Log("No actions have been implemented");
                break;
        }

        if (holdCheck == true && action == "Crowbar")
        {
            crowbarFillBar.gameObject.SetActive(true);
            CrowbarUse();
        }
        else
        {
            crowbarTimer = 2;
            crowbarFillBar.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Reset variables and destory extra things.
    /// </summary>
    private void OnDisable()
    {
        actionItem = null;
        action = null;

        if (waterRangeDisplay != null)
        {
            Destroy(waterRangeDisplay);
            waterRangeDisplay = null;
        }

        crowbarFillBar.gameObject.SetActive(false);

        if (extinguisherRangeDisplay != null)
        {
            Destroy(extinguisherRangeDisplay);
            extinguisherRangeDisplay = null;
        }

        GetComponent<PlayerMovementScript>().ChangeClampMoveSettings(1, -1, 1, -1);
    }

    /// <summary>
    /// Executes an action.
    /// </summary>
    /// <param name="context"></param>
    private void OnAction(InputValue context)
    {
        if (context.isPressed == true)
        {
            switch (action)
            {
                case ("Axe"):
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

                //case ("Crowbar"):
                //    CrowbarUse();
                //    break;

                // Added by: Jacob Biles to cover default/unimplemented items
                default:
                    //Debug.Log("No actions have been implemented");
                    break;
            }
        }
        holdCheck = context.isPressed;
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
                    waterRangeDisplay.GetComponent<WaterBucketRangeScript>().GetPlayer(gameObject);
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
        LayerMask layerMask = LayerMask.GetMask("Default");
        Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f, layerMask);
        Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);


        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "BreakableObject")
            {
                hit.collider.gameObject.GetComponent<BreakableObjectScript>().DamageBreakable();
                PlayAudio(axeChopSound);
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
            Vector3[] tiles = waterRangeDisplay.GetComponent<WaterBucketRangeScript>().ReturnItemLocations();
            for (int i = 0; i < tiles.Length; i++)
            {
                var childLocation = tiles[i];
                Instantiate<GameObject>(waterColliderPrefab, childLocation, Quaternion.identity);
            }
            PlayAudio(waterBucketSound);
            actionItem.GetComponent<WaterBucketScript>().EmptyBucket();
            Destroy(waterRangeDisplay);
        }
        else
        {
            Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f);
            Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "DropOff")
                {
                    actionItem.GetComponent<WaterBucketScript>().FillBucket();
                    waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab, transform.position, Quaternion.identity);
                    waterRangeDisplay.GetComponent<WaterBucketRangeScript>().GetPlayer(gameObject);
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

    /// <summary>
    /// Uses the fire Extinguisher to put out fires
    /// </summary>
    private void ExtinguisherUse()
    {
        Vector3 childLocation = extinguisherRangeDisplay.transform.GetChild(0).transform.position;
        Instantiate<GameObject>(extinguisherColliderPrefab, childLocation, Quaternion.identity);
        PlayAudio(extinguisherSpraySound);
    }

    /// <summary>
    /// breaks with the crowbar
    /// </summary>
    private void CrowbarUse()
    {
        LayerMask layerMask = LayerMask.GetMask("Default");
        Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f, layerMask);
        Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);

        Debug.Log(hit.collider);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "CrowbarObject")
            {
                if (crowbarTimer > 0)
                {
                    crowbarTimer -= Time.deltaTime;
                    crowbarFillBar.fillAmount = (crowbarTimer / 2);
                }
                else
                {
                    hit.collider.gameObject.SetActive(false);
                    PlayAudio(crowbarPrySound);
                }
            }
            else
            {
                crowbarTimer = 2;
                crowbarFillBar.fillAmount = (crowbarTimer / 2);
            }
        }
        else
        {
            crowbarTimer = 2;
            crowbarFillBar.fillAmount = (crowbarTimer / 2);
        }
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

    /// <summary>
    /// Mess with the player movement limits and speed when they are carrying heavy debris together
    /// </summary>
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

    /// <summary>
    /// Show the player the location the extinguisher will put out
    /// </summary>
    private void ExtinguisherHave()
    {
        Vector3 facingDisplace = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        Vector3Int extinguisherSpawnLocation = grid.WorldToCell(transform.position + facingDisplace);

        extinguisherRangeDisplay.transform.position = grid.CellToWorld(extinguisherSpawnLocation);
    }

    private void Debris1PHave()
    {
        GameObject debris = actionItem.GetComponent<SinglePlayerPushPickUp>().OriginalParent;

        int[] modifier = debris.GetComponent<SinglePlayerPushScript>().SavedModifier;

        if (!debris.GetComponent<SinglePlayerPushScript>().CollisionState)
        {
            GetComponent<PlayerMovementScript>().ChangeClampMoveSettings(modifier[0], modifier[0], modifier[1], modifier[1]);
        }
        else
        {
            GetComponent<PlayerMovementScript>().ChangeClampMoveSettings(0, 0, 0, 0);
        }

        //Vector3 position = Vector3.zero;

        //if (modifier[0] != 0)
        //{
        //    position.x = Mathf.Clamp(transform.position.x, debris.transform.position.x - (modifier[0] * 1.1f), debris.transform.position.x - (modifier[0] * 1.1f));
        //    position.y = debris.transform.position.y;
        //}
        //else
        //{
        //    //position.y = Mathf.Clamp(transform.position.y, debris.transform.position.y - (modifier[1]*1.3f), debris.transform.position.y - (modifier[1]*1.2f));
        //    position.y = transform.position.y;
        //    position.x = debris.transform.position.x;
        //}

        //transform.position = position;
    }

    /// <summary>
    /// Plays a Sound
    /// </summary>
    /// <param name="clip"></param>
    private void PlayAudio(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        soundEffectSource.clip = clip;
        soundEffectSource.Play();
    }

    /// <summary>
    /// Tell whatever calls the method what the action item is
    /// </summary>
    /// <returns></returns>
    public string ReturnActionString()
    {
        if (action != null)
        {
            return action;
        }
        else
        {
            return "null";
        }
    }
}