using UnityEngine;

public class LadderCoverPit : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private GameObject ladder;
    private BoxCollider2D boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ladder != null)
        {
            if (ladder.transform.position != transform.position && boxCollider.enabled == false)
            {
                boxCollider.enabled = true;
            }
        }
    }

    /// <summary>
    /// This allows the ladder to go over the hole.
    /// </summary>
    /// <param name="ladderObject"></param>
    public void ReceiveLadder(GameObject ladderObject)
    {
        ladder = ladderObject;
        ladder.GetComponent<LadderScript>().DropThePickups();
        ladder.transform.position = transform.position;
        ladder.transform.rotation = transform.rotation;
        ladder.GetComponent<LadderScript>().ActivateHolePickups();

        boxCollider.enabled = false;
    }
}
