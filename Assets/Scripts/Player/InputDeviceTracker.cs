using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceTracker : MonoBehaviour
{

    #region Fields

    public static bool gamepadConnected = false;
    public static Action<bool> ControllerConnected = delegate { };

    #endregion
    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;

        
    }



    private void Start()
    {
        foreach (var device in InputSystem.devices)
        {
            OnDeviceChange(device, InputDeviceChange.Added);
        }
    }



    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }


    // Method called when a device is added, removed, disconnected, or reconnected
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad added");
                    gamepadConnected = true;
                    ControllerConnected(true);
                }
                else if (device is Keyboard)
                {
                    Debug.Log("Changed to keyboard");
                    if(!gamepadConnected)
                    {
                        gamepadConnected = false;
                        ControllerConnected(false);
                    }
                }
                break;

            case InputDeviceChange.Removed:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad removed");
                    gamepadConnected = false;
                    ControllerConnected(false);
                }
                break;

            case InputDeviceChange.Disconnected:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad disconnected");
                    gamepadConnected = false;
                    ControllerConnected(false);
                }
                break;

            case InputDeviceChange.Reconnected:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad Reconnected");
                    gamepadConnected = true;
                    ControllerConnected(true);
                }
                break;
        }
    }
}