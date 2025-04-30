using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "Scriptable Objects/Pickup")]
public class Pickup : ScriptableObject
{
    public int score;
    public bool carryable;
    public bool winCon;
    public bool passable;
    public bool heavy;
    public bool destructible;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        
        
    }

    // Update is called once per frame
    void OnDisable()
    {
        
    }
}
