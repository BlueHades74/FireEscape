using System;
using UnityEngine;

public class PlayerEventSystem : MonoBehaviour
{
    public static PlayerEventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnObjectPickedUp;

    public void ObjectPickedUp()
    {
        OnObjectPickedUp?.Invoke();
    }
}
