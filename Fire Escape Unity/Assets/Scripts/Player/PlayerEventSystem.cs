using System;
using UnityEngine;

public class PlayerEventSystem : MonoBehaviour
{
    public static PlayerEventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action<Vector3> OnObjectPickedUp;

    public void ObjectPickedUp(Vector3 transform)
    {
        OnObjectPickedUp?.Invoke(transform);
    }
}
