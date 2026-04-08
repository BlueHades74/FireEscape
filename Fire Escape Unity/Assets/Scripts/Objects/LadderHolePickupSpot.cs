using UnityEngine;

public class LadderHolePickupSpot : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    [SerializeField]
    private GameObject[] pickups;

    [SerializeField]
    private GameObject ladder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure both pickups are picked up before removing the ladder from the hole
        if (pickups[0].transform.parent != transform.parent && pickups[1].transform.parent != transform.parent && pickups[0].transform.parent != pickups[1].transform.parent && pickups[0].transform.parent != null && pickups[1].transform.parent != null)
        {
            pickups[0].GetComponent<ObjectManager>().DropItem();
            pickups[0].GetComponent<LadderHolePickup>().GoBackToParent();
            pickups[1].GetComponent<ObjectManager>().DropItem();
            pickups[1].GetComponent<LadderHolePickup>().GoBackToParent();

            Vector3 pos = transform.position;
            pos.z = 0;
            ladder.GetComponent<LadderScript>().DeactivateHolePickups(pos);
        }
    }
}
