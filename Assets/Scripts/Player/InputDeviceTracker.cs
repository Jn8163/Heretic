using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceTracker : MonoBehaviour
{
    #region Fields

    private static InputDeviceTracker instance;
    private InputDevice lastUsedDevice;
    public static bool gamepadActive = false;
    public static event Action<bool> ControllerConnected = delegate { };

    private Gamepad currentGamepad;
    private Mouse currentMouse;

    #endregion

    #region Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        DetectInitialDevice();
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
            case InputDeviceChange.Reconnected:
                Debug.Log($"Device {device.name} connected");
                break;
            case InputDeviceChange.Removed:
            case InputDeviceChange.Disconnected:
                Debug.Log($"Device {device.name} disconnected");
                if (device == lastUsedDevice)
                {
                    lastUsedDevice = null;
                    UpdateGamepadStatus();
                }
                break;
        }
    }

    private void Update()
    {
        // Track actual input from the devices
        if (Gamepad.current != null && Gamepad.current.leftStick.ReadValue().magnitude > 0)
        {
            SetActiveDevice(Gamepad.current);
        }
        else if (Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero)
        {
            SetActiveDevice(Mouse.current);
        }
    }

    private void SetActiveDevice(InputDevice device)
    {
        if (device != lastUsedDevice)
        {
            lastUsedDevice = device;
            UpdateGamepadStatus();
        }
    }

    private void DetectInitialDevice()
    {
        if (Gamepad.current != null)
        {
            lastUsedDevice = Gamepad.current;
            gamepadActive = true;
        }
        else if (Keyboard.current != null)
        {
            lastUsedDevice = Keyboard.current;
            gamepadActive = false;
        }
        ControllerConnected(gamepadActive);
    }

    private void UpdateGamepadStatus()
    {
        bool isGamepad = lastUsedDevice is Gamepad;
        if (gamepadActive != isGamepad)
        {
            gamepadActive = isGamepad;
            ControllerConnected(gamepadActive);
            Debug.Log($"Gamepad Active: {gamepadActive}");
        }
    }

    #endregion
}