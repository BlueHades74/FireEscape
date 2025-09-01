using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private Vector3 positionVector;

    private Vector3[] playerLastPosition;

    [SerializeField]
    private GameObject[] pickups;

    [SerializeField]
    private GameObject[] holePickups;

    private Rigidbody2D rb;

    private bool isFullyPickedUp;

    public bool IsFullyPickedUp { get => isFullyPickedUp; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionVector = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerLastPosition = new Vector3[2];
    }

    // Update is called once per frame
    void Update()
    {
        //How the ladder moves
        if (pickups[0].transform.parent != null && pickups[1].transform.parent != null && pickups[0].transform.parent != pickups[1].transform.parent)
        {
            isFullyPickedUp = true;

            Vector3 object1Position = pickups[0].transform.parent.transform.position;
            Vector3 object2Position = pickups[1].transform.parent.transform.position;

            positionVector = (object1Position + object2Position) / 2;

            //transform.position = positionVector;

            Vector3 directionForCast = positionVector - transform.position;

            BoxCollider2D bx = GetComponent<BoxCollider2D>();
            RaycastHit2D[] results = new RaycastHit2D[5];
            if (bx.Cast(directionForCast, results, 0.1f) > 2 && CheckCollisionsForTag(results, "Wall"))
            {
                if (results[2].collider.gameObject.tag == "Wall")
                {
                    pickups[0].transform.parent.transform.position = playerLastPosition[0];
                    pickups[1].transform.parent.transform.position = playerLastPosition[1];
                }
            }
            else
            {
                playerLastPosition[0] = object1Position;
                playerLastPosition[1] = object2Position;
                transform.position = positionVector;
            }

            // Calculate the difference in positions of the pickup points, not the parents.
            // This determines the ladder's orientation based on where its ends are.
            float ladderLengthX = pickups[0].transform.position.x - pickups[1].transform.position.x;
            float ladderLengthY = pickups[0].transform.position.y - pickups[1].transform.position.y;

            // float hypotenuse = Mathf.Sqrt((ladderLengthX * ladderLengthX) + (ladderLengthY * ladderLengthY)); // This line is not used

            // Use Mathf.Atan2 to safely calculate the angle in radians
            // and then convert to degrees for Quaternion.Euler.
            // Atan2 handles cases where ladderLengthX is 0, preventing division by zero.
            float angleInDegrees = Mathf.Atan2(ladderLengthY, ladderLengthX) * Mathf.Rad2Deg;
            Quaternion ladderRotation = Quaternion.Euler(0, 0, angleInDegrees);

            transform.rotation = ladderRotation;
        }
        else
        {
            isFullyPickedUp = false;
        }
    }

    /// <summary>
    /// Force the players to drop their pickups.
    /// </summary>
    public void DropThePickups()
    {
        pickups[0].GetComponent<ObjectManager>().DropItem();
        pickups[1].GetComponent<ObjectManager>().DropItem();
    }

    /// <summary>
    /// Makes sure that the ladder can be picked up correctly when over the hole.
    /// </summary>
    public void ActivateHolePickups()
    {
        pickups[0].SetActive(false);
        pickups[1].SetActive(false);

        holePickups[0].SetActive(true);
        holePickups[1].SetActive(true);

        holePickups[0].transform.position = new Vector3(transform.position.x - 3, transform.position.y);
        holePickups[1].transform.position = new Vector3(transform.position.x + 3, transform.position.y);
    }

    /// <summary>
    /// Returns to normal style of ladder pickup after it is removed from the hole.
    /// </summary>
    /// <param name="newLocation"></param>
    public void DeactivateHolePickups(Vector3 newLocation)
    {
        pickups[0].SetActive(true);
        pickups[1].SetActive(true);

        transform.position = newLocation;

        holePickups[0].SetActive(false);
        holePickups[1].SetActive(false);
    }

    private bool CheckCollisionsForTag(RaycastHit2D[] array, string tag)
    {
        for (int i = 0; i < array.Length; i++)
        {
            try
            {
                if (array[i].collider.gameObject.tag == tag)
                {
                    return true;
                }
            }
            catch { }
        }
        return false;
    }
}
