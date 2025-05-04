using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private int playerIndex;

    private ControlMap inputActions;

    public int PlayerIndex { get => playerIndex; }
    public ControlMap InputActions { get => inputActions; }

    private void Awake()
    {
        inputActions = new ControlMap();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<PlayerMovementScript>().enabled = true;
        GetComponent<PlayerInteraction>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        inputActions?.Enable();
        
    }

    private void OnDisable()
    {
        inputActions?.Disable();
    }
}
