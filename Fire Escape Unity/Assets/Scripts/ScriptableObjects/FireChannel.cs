using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FireChannel", menuName = "Scriptable Objects/FireChannel")]
public class FireChannel : ScriptableObject
{
    public event UnityAction InteractEvent;

    public void OnInteract()
    {
        if(InteractEvent != null)
            InteractEvent.Invoke();
    }




    
}
