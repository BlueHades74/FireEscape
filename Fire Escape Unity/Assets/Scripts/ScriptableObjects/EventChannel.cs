using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventChannel", menuName = "Scriptable Objects/EventChannel")]
public class EventChannel : ScriptableObject
{
    public event UnityAction<Vector3> OnObjectPickedUp;
    public void ObjectPickedUp(Vector3 transform)
    {
        OnObjectPickedUp?.Invoke(transform);
    }
}
