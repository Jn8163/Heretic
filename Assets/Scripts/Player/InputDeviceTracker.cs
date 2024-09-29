using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceTracker : MonoBehaviour
{

    #region Fields

    public static string currentDevice = "keyboard";
    public static Action<bool> ControllerConnected = delegate { };

    #endregion
    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;

        foreach (var device in InputSystem.devices)
        {
            OnDeviceChange(device, InputDeviceChange.Added);
        }
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }


    // Method called when a device is added, removed, or changed
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad added");
                    currentDevice = "gamepad";
                    ControllerConnected(true);
                }
                else if (device is Keyboard)
                {
                    Debug.Log("keyboard added");
                    if(currentDevice != "gamepad")
                    {
                        currentDevice = "keyboard";
                        ControllerConnected(false);
                    }
                }
                break;

            case InputDeviceChange.Removed:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad removed");
                    currentDevice = "keyboard";
                    ControllerConnected(false);
                }
                break;

            case InputDeviceChange.Disconnected:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad disconnected");
                    currentDevice = "keyboard";
                    ControllerConnected(false);
                }
                break;

            case InputDeviceChange.Reconnected:
                if (device is Gamepad)
                {
                    Debug.Log("gamepad Reconnected");
                    currentDevice = "gamepad";
                    ControllerConnected(true);
                }
                break;
        }
    }
}