using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActionScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles
    //Audio Relate Hoohah added by henry cummings

    [Header("General")]
    [SerializeField]
    private AudioSource soundEffectSource;

    private GameObject actionItem;
    private PlayerInputController inputs;

    [SerializeField]
    private Grid grid;

    private string action;

    private bool holdCheck;

    [Header("Axe")]
    [SerializeField]
    private AudioClip axeChopSound;

    [Header("WaterBucket")]
    [SerializeField]
    private GameObject[] waterRangePrefab;
    [SerializeField]
    private GameObject waterColliderPrefab;
    private GameObject waterRangeDisplay;
    [SerializeField]
    private AudioClip[] waterBucketSounds;
    [SerializeField]
    private AudioClip waterSloshSound;
    [SerializeField]
    private AudioClip refillSound;
    private int waterColliderRotation = 0;

    [Header("Crowbar")]
    private float crowbarTimer;
    private Image crowbarFillBar;
    [SerializeField]
    private AudioClip crowbarPrySound;

    [Header("Extinguisher")]
    [SerializeField]
    private GameObject extinguisherRangePrefab;
    [SerializeField]
    private GameObject extinguisherColliderPrefab;
    private GameObject extinguisherRangeDisplay;
    [SerializeField]
    private AudioClip extinguisherSpraySound;

    private Vector2 boxColliderSizeOrig;
    private Vector2 boxColliderOffsetOrig;
    private BoxCollider2D bx;

    [Header("2 player Carry object")]
    [SerializeField]
    private float carry2PColliderSizeMod;
    [SerializeField]
    private float carry2PColliderOffsetMod;
    [SerializeField]
    private AudioClip carry2pSound;

    private void Awake()
    {
        bx = GetComponent<BoxCollider2D>();
        boxColliderSizeOrig = bx.size;
        boxColliderOffsetOrig = bx.offset;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crowbarFillBar = transform.GetChild(2).transform.GetChild(0).GetComponent<Image>();
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

            case ("Hose"):
                HoseNozzleHave();
                break;

            case ("HoseP2Spot"):
                HoseP2SpotHave();
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

        GetComponent<PlayerMovementScript>().ChangeAddedVelocity(Vector2.zero);
        GetComponent<PlayerMovementScript>().ChangeClampMoveSettings(1, -1, 1, -1);
        GetComponent<PlayerMovementScript>().SwitchFaceDirection(true);
        bx.size = boxColliderSizeOrig;
        bx.offset = boxColliderOffsetOrig;
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

                case ("HoseP2Spot"):
                    HoseP2SpotUse(); 
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

        if (waterRangeDisplay != null)
        {
            Destroy(waterRangeDisplay);
            waterRangeDisplay = null;
        }

        if (bx.size !=  boxColliderSizeOrig)
        {
            bx.size = boxColliderSizeOrig;
            bx.offset = boxColliderOffsetOrig;
        }

        switch (action)
        {
            case ("Bucket"):
                if (actionItem.GetComponent<WaterBucketScript>().IsFilled == true)
                {
                    waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab[actionItem.GetComponent<WaterBucketScript>().CurrentCharges - 1], transform.position, Quaternion.identity);
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
            var selectedSound = Random.Range(0, waterBucketSounds.Length);
            PlayAudio(waterBucketSounds[selectedSound]);
            actionItem.GetComponent<WaterBucketScript>().CurrentCharges--;
            Destroy(waterRangeDisplay);
            if (actionItem.GetComponent<WaterBucketScript>().CurrentCharges <= 0)
            {
                actionItem.GetComponent<WaterBucketScript>().EmptyBucket();
            }
            else
            {
                waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab[actionItem.GetComponent<WaterBucketScript>().CurrentCharges - 1], transform.position, Quaternion.identity);
            }
        }
        //else
        //{
        //    Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        //    RaycastHit2D hit = Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f, LayerMask.GetMask("Default"));
        //    Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);

        //    if (hit.collider != null)
        //    {
        //        if (hit.collider.gameObject.tag == "DropOff")
        //        {
        //            actionItem.GetComponent<WaterBucketScript>().FillBucket();
        //            waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab[actionItem.GetComponent<WaterBucketScript>().CurrentCharges - 1], transform.position, Quaternion.identity);
        //            waterRangeDisplay.GetComponent<WaterBucketRangeScript>().GetPlayer(gameObject);
        //        }
        //    }
        //}
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

    private void HoseP2SpotUse()
    {
        HoseP2SpotScript script = actionItem.GetComponent<HoseP2SpotScript>();

        script.SwapModifier();
    }

    /// <summary>
    /// For when the player has the bucket, does the grid thing.
    /// </summary>
    private void BucketHave()
    {
        if (actionItem.GetComponent<WaterBucketScript>().IsFilled)
        {
            Vector3 facingDisplace = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x * 0.2f, GetComponent<PlayerMovementScript>().FacingDirection.y * 0.2f, 0);
            Vector3Int waterSpawnLocation = grid.WorldToCell(transform.position);

            Vector2 direction = GetComponent<PlayerMovementScript>().FacingDirection;

            if (direction.x < 0)
            {
                waterColliderRotation = 90;
            }
            else if (direction.x > 0)
            {
                waterColliderRotation = 270;
            }
            else if (direction.y < 0)
            {
                waterColliderRotation = 180;
            }
            else if (direction.y > 0)
            {
                waterColliderRotation = 0;
            }

            waterRangeDisplay.transform.position = transform.position;
            waterRangeDisplay.transform.rotation = Quaternion.Euler(0, 0, waterColliderRotation);

            if (GetComponent<Rigidbody2D>().linearVelocity != Vector2.zero)
            {
                if (soundEffectSource != null)
                {
                    if (soundEffectSource.isPlaying == false)
                    {
                        PlayAudio(waterSloshSound);
                    }
                }
            }
            else if (soundEffectSource != null)
            {
                if (soundEffectSource.clip == waterSloshSound)
                {
                    soundEffectSource.Stop();
                }
            }
        }

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f, LayerMask.GetMask("Default"));
        //Debug.Log(hit.collider);

        //if (hit.collider != null)
        //{
        //    if (hit.collider.gameObject.tag == "DropOff")
        //    {
        //        if (waterRangeDisplay != null)
        //        {
        //            Destroy(waterRangeDisplay);
        //            waterRangeDisplay = null;
        //        }

        //        actionItem.GetComponent<WaterBucketScript>().FillBucket();
        //        waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab[actionItem.GetComponent<WaterBucketScript>().CurrentCharges - 1], transform.position, Quaternion.identity);
        //        waterRangeDisplay.GetComponent<WaterBucketRangeScript>().GetPlayer(gameObject);
        //    }
        //}
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
        //1 Size
        //X:2.022391
        //Y:0.6082556

        //1 Offset
        //X:0.8383136
        //Y:-0.1174277

        //2 Size
        //X:1.935609
        //Y:0.7136691

        //2 Offset
        //X:-0.7097052
        //Y:-0.1334451

        //1.95
        //0.75

        GameObject debris = actionItem.GetComponent<DebrisPickup>().OriginalParent;

        if (debris.GetComponent<DebrisScript>().IsCarriedByTwoPlayers == false)
        {
            GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0);
            bx.size = boxColliderSizeOrig;
            bx.offset = boxColliderOffsetOrig;
        }
        else
        {
            GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0.7f);
            if (GetComponent<Rigidbody2D>().linearVelocity != Vector2.zero)
            {
                PlayAudio(carry2pSound);
            }
        }

        Vector3 position = Vector3.zero;

        if ((debris.transform.rotation.z < 0.1 && debris.transform.rotation.z > -0.1))
        {
            if (debris.transform.position.x > transform.position.x)
            {
                position.x = Mathf.Clamp(transform.position.x, debris.transform.position.x - 1.9f, debris.transform.position.x - 1.8f);

                bx.offset = new Vector2(carry2PColliderOffsetMod, bx.offset.y);
            }
            else
            {
                position.x = Mathf.Clamp(transform.position.x, debris.transform.position.x + 1.8f, debris.transform.position.x + 1.9f);

                bx.offset = new Vector2(-carry2PColliderOffsetMod, bx.offset.y);
            }

            position.y = Mathf.Clamp(transform.position.y, debris.transform.position.y - 0.3f, debris.transform.position.y + 0.3f);

            bx.size = new Vector2(carry2PColliderSizeMod, bx.size.y);
        }
        else
        {
            if (debris.transform.position.y > transform.position.y)
            {
                position.y = Mathf.Clamp(transform.position.y, debris.transform.position.y - 1.9f, debris.transform.position.y - 1.8f);

                bx.offset = new Vector2(bx.offset.x, carry2PColliderOffsetMod);
            }
            else
            {
                position.y = Mathf.Clamp(transform.position.y, debris.transform.position.y + 1.8f, debris.transform.position.y + 1.9f);

                bx.offset = new Vector2(bx.offset.x, -carry2PColliderOffsetMod);
            }

            position.x = Mathf.Clamp(transform.position.x, debris.transform.position.x - 0.3f, debris.transform.position.x + 0.3f);

            bx.size = new Vector2(bx.size.x, carry2PColliderSizeMod);
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

        extinguisherRangeDisplay.transform.position = transform.position + facingDisplace;
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
    }

    private void HoseNozzleHave()
    {
        HoseNozzleScript hoseScript =  actionItem.GetComponent<HoseNozzleScript>();
        PlayerMovementScript moveScript= GetComponent<PlayerMovementScript>();

        Vector2 pushback = moveScript.FacingDirection * -hoseScript.CurrentPushback;
        moveScript.ChangeAddedVelocity(pushback);

        hoseScript.SetRotation(moveScript.FacingDirection);

        if(hoseScript.CurrentPercentage > 0)
        {
            moveScript.SwitchFaceDirection(false);
        }
        else
        {
            moveScript.SwitchFaceDirection(true);
        }    
    }

    private void HoseP2SpotHave()
    {
        GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(0);
    }

    /// <summary>
    /// Plays a Sound
    /// </summary>
    /// <param name="clip"></param>
    private void PlayAudio(AudioClip clip)
    {
        if(soundEffectSource == null)
        {
            return;
        }
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

    /// <summary>
    /// Detects triggers to fill bucket.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (action == "Bucket")
        {
            if (collision.CompareTag("DropOff"))
            {
                if (waterRangeDisplay != null)
                {
                    Destroy(waterRangeDisplay);
                    waterRangeDisplay = null;
                }

                actionItem.GetComponent<WaterBucketScript>().FillBucket();

                if (refillSound != null)
                {
                    PlayAudio(refillSound);
                }

                waterRangeDisplay = Instantiate<GameObject>(waterRangePrefab[actionItem.GetComponent<WaterBucketScript>().CurrentCharges - 1], transform.position, Quaternion.identity);
                waterRangeDisplay.GetComponent<WaterBucketRangeScript>().GetPlayer(gameObject);
            }
        }
    }
}