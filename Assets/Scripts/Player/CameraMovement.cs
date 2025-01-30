using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject player;
    [SerializeField] private float sensitivity = 15f, gamepadSensitivity = 150f;
    private PlayerInput pInput;
    private float verticalRotation;
    private bool gamepadActive;

    #endregion



    #region Methods

    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        InputDeviceTracker.ControllerConnected += ControllerConnected;
    }



    private void OnDisable()
    {
        pInput.Disable();

        InputDeviceTracker.ControllerConnected -= ControllerConnected;

    }



    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }



    private void Update()
    {
        Rotation();
    }



    private void Rotation()
    {
        //Retrieve input
        float horizontalMov, verticalMov;

        if (gamepadActive)
        {
            horizontalMov = pInput.Player.Look.ReadValue<Vector2>().x * (gamepadSensitivity+ (PlayerPrefs.GetFloat("Sense")*15)) * Time.deltaTime;
            verticalMov = pInput.Player.Look.ReadValue<Vector2>().y * (gamepadSensitivity + (PlayerPrefs.GetFloat("Sense") * 15)) * Time.deltaTime;
        }
        else
        {
            horizontalMov = pInput.Player.Look.ReadValue<Vector2>().x * (sensitivity + (PlayerPrefs.GetFloat("Sense"))) * Time.deltaTime;
            verticalMov = pInput.Player.Look.ReadValue<Vector2>().y * (sensitivity + (PlayerPrefs.GetFloat("Sense")))* Time.deltaTime;
        }

        //Vertical rotation
        verticalRotation -= verticalMov;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Horizontal rotation
        player.transform.Rotate(Vector3.up * horizontalMov);
    }
    


    private void ControllerConnected(bool b)
    {
        gamepadActive = b;
    }

    #endregion
}