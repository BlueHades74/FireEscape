using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerActionScript : MonoBehaviour
{
    private GameObject actionItem;
    private PlayerInputController inputs;

    [SerializeField]
    private Grid grid;

    private string action;

    [SerializeField]
    private GameObject rangePrefab;
    [SerializeField]
    private GameObject waterColliderPrefab;
    private GameObject waterRangeDisplay;

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
        }
    }

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
    }

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
        }
    }

    public void ReceiveActionItem(GameObject actionItemSent)
    {
        actionItem = actionItemSent;

        action = actionItem.GetComponent<ObjectManager>().Action;

        switch (action)
        {
            case ("Bucket"):
                waterRangeDisplay = Instantiate<GameObject>(rangePrefab, transform.position, Quaternion.identity);
                break;
        }
    }

    public void AxeUse()
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

    public void BucketUse()
    {

        for (int i = 0; i < waterRangeDisplay.transform.childCount; i++)
        {
            var childLocation = waterRangeDisplay.transform.GetChild(i).transform.position;
            Instantiate<GameObject>(waterColliderPrefab, childLocation, Quaternion.identity);
        }
    }

    public void BucketHave()
    {
        Vector3 facingDisplace = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        Vector3Int waterSpawnLocation = grid.WorldToCell(transform.position + facingDisplace);

        waterRangeDisplay.transform.position = grid.CellToWorld(waterSpawnLocation);
    }
}
