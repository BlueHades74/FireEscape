using UnityEngine;

public class WaterBucketRangeScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private GameObject[] children;
    private float nearestPosition;
    private GameObject player;
    private GameObject closestChild;
    private GameObject exClosest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        children = new GameObject[transform.childCount];

        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (closestChild != null)
        {
            exClosest = closestChild;
        }

        FindClosest();

        if (exClosest != closestChild || exClosest == null)
        {
            if (exClosest != null)
            {
                exClosest.gameObject.SetActive(true);
            }
            closestChild.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Finds which child is closest to the player.
    /// </summary>
    private void FindClosest()
    {
        nearestPosition = 0;
        closestChild = null;

        for (int i = 0; i < children.Length; i++)
        {
            float pos = Vector2.Distance(children[i].transform.position, player.transform.position);
            if (nearestPosition > pos || closestChild == null)
            {
                nearestPosition = pos;
                closestChild = children[i];
            }
        }
    }

    /// <summary>
    /// Recieves what player is holding the bucket.
    /// </summary>
    /// <param name="playerHolding"></param>
    public void GetPlayer(GameObject playerHolding)
    {
        player = playerHolding;
    }

    /// <summary>
    /// Returns where the active tiles are.
    /// </summary>
    /// <returns></returns>
    public Vector3[] ReturnItemLocations()
    {
        Vector3[] itemLocations = new Vector3[4];
        int a = 0;

        for (int i = 0;i < children.Length;i++)
        {
            if (children[i].gameObject.activeSelf)
            {
                itemLocations[a] = children[i].transform.position;
                a++;
            }
        }

        return itemLocations;
    }
}
