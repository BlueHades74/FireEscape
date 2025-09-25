using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceManager : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private GameObject[] players;
    private InputDevice[] inputDevices;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        List<InputDevice> devices = new List<InputDevice>();

        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            string type = InputSystem.devices[i].description.deviceClass;
            if (type == "Keyboard" || type == "")
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

        RebindControllers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RebindControllers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerInputController>().PairWithDevice(inputDevices[(1 + i) % inputDevices.Length], inputDevices.Length);
        }
    }
}
