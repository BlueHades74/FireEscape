using UnityEngine;

public class QuestNPC : NPCBase
{
    public GameObject questTarget;
    public float turnInRange;

    void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, questTarget.transform.position);
        
        if (distanceToTarget < turnInRange)
        {
            Debug.Log("Yay you did it");
        }
    }
}
