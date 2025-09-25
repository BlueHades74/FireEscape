using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputController : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    [SerializeField]
    private int playerIndex;

    private InputUser user;

    public int PlayerIndex { get => playerIndex; }

    private void Awake()
    {
        //Figure out who the user is
        user = GetComponent<PlayerInput>().user;
    }

    public void PairWithDevice(InputDevice device, int deviceCount)
    {
        InputUser.PerformPairingWithDevice(device, user);
        GetComponent<PlayerInput>().SwitchCurrentControlScheme(device);

        if (deviceCount == 1)
        {
            if (user.index == 0)
            {
                GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard - Left", Keyboard.current);
            }
            else
            {
                GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard - Right", Keyboard.current);
            }
        }
    }
}
