using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceTracker : MonoBehaviour
{

    #region Fields

    private string currentDevice = "keyboard";
    public Action<InputDevice> InputDeviceChanged = delegate { };

    #endregion
    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
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
                Debug.Log("Device added: " + device.name);
                if(currentDevice != "gamepad")
                {
                    currentDevice = device.name;
                }
                break;

            case InputDeviceChange.Removed:
                Debug.Log("Device removed: " + device.name);
                currentDevice = "keyboard";
                break;

            case InputDeviceChange.Disconnected:
                Debug.Log("Device disconnected: " + device.name);
                break;

            case InputDeviceChange.Reconnected:
                Debug.Log("Device reconnected: " + device.name);
                break;
        }
    }
}