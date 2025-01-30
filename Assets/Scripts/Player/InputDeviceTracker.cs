using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceTracker : MonoBehaviour
{
    #region Fields

    private static InputDeviceTracker instance;
    public static bool gamepadConnected = false;
    public static Action<bool> ControllerConnected = delegate { };

    #endregion

    #region Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CheckAllCurrentDevices();
        }
        else if (instance != this)
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable Gamepad stateGamepad Connected: " + gamepadConnected);
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void Start()
    {
        Debug.Log("Start Gamepad stateGamepad Connected: " + gamepadConnected);

        // Notify listeners of the current gamepad state.
        ControllerConnected(gamepadConnected);
    }

    // Method called when a device is added, removed, disconnected, or reconnected
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                if (device is Gamepad)
                {
                    Debug.Log("Gamepad added");
                    Debug.Log(device.name);
                    gamepadConnected = true;
                    ControllerConnected(true);
                }
                else if (device is Keyboard)
                {
                    Debug.Log("Changed to keyboard");
                    if (!gamepadConnected)
                    {
                        ControllerConnected(false);
                    }
                }
                break;

            case InputDeviceChange.Removed:
                if (device is Gamepad)
                {
                    Debug.Log("Gamepad removed");
                    gamepadConnected = false;
                    ControllerConnected(false);
                }
                break;

            case InputDeviceChange.Disconnected:
                if (device is Gamepad)
                {
                    Debug.Log("Gamepad disconnected");
                    gamepadConnected = false;
                    ControllerConnected(false);
                }
                break;

            case InputDeviceChange.Reconnected:
                if (device is Gamepad)
                {
                    Debug.Log("Gamepad reconnected");
                    gamepadConnected = true;
                    ControllerConnected(true);
                }
                break;
        }
    }

    private void CheckAllCurrentDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            OnDeviceChange(device, InputDeviceChange.Added);
        }
    }

    #endregion
}