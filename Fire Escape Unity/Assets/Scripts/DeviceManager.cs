using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.XInput;

public class DeviceManager : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private GameObject[] players;
    private InputDevice[] inputDevices;

    private int deviceCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.LogWarning(players[0].name + ", " + players[1].name);

        if (players[0].name == "Player 1")
        {
            GameObject player1 = players[0];
            players[0] = players[1];
            players[1] = player1;
        }


        ValidateDevicesAndBind();
    }

    // Update is called once per frame
    void Update()
    {
        if (deviceCount != InputSystem.devices.Count)
        {
            ValidateDevicesAndBind();
        }
    }

    /// <summary>
    /// Binds Controllers to the players
    /// </summary>
    private void RebindControllers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerInputController>().PairWithDevice(inputDevices[(1 + i) % inputDevices.Length], inputDevices.Length);
        }
    }

    /// <summary>
    /// Checks all input devices to see if they are valid
    /// </summary>
    private void ValidateDevicesAndBind()
    {
        VerifyInputIntegrity();
        deviceCount = InputSystem.devices.Count;

        List<InputDevice> devices = new List<InputDevice>();

        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            string type = InputSystem.devices[i].description.deviceClass;
            if ((type == "Keyboard" || type == "") && InputSystem.devices[i].enabled == true)
            {
                devices.Add(InputSystem.devices[i]);
            }
        }

        inputDevices = new InputDevice[devices.Count];
        for (int i = 0; i < devices.Count; i++)
        {
            Debug.LogWarning(devices[i]);
            inputDevices[i] = devices[i];
            Debug.LogWarning(inputDevices[i]);
        }

        print(inputDevices);

        RebindControllers();
    }

    /// <summary>
    /// Verifies that controllers don't count twice
    /// </summary>
    private static void VerifyInputIntegrity()
    {
        InputDevice lastDevice = null;

        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            if (InputSystem.devices[i] is SwitchProControllerHID)
            {
                if (lastDevice is XInputController)
                {
                    InputSystem.DisableDevice(lastDevice);
                }
            }

            lastDevice = InputSystem.devices[i];
        }
    }
}
