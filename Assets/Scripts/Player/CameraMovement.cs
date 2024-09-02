using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject player;
    [SerializeField] private float sensitivity = 100f;
    private Vector3 startPos;
    private InputSystem pInput;
    private float verticalRotation;

    #endregion



    #region Methods

    private void OnEnable()
    {
        pInput = new InputSystem();
        pInput.Enable();
    }



    private void OnDisable()
    {
        pInput.Disable();
    }



    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        startPos = transform.position;
    }



    private void Update()
    {
        Rotation();
    }



    private void Rotation()
    {
        //Retrieve input
        float horizontalMov = pInput.Player.Look.ReadValue<Vector2>().x * sensitivity * Time.deltaTime;
        float verticalMov = pInput.Player.Look.ReadValue<Vector2>().y * sensitivity * Time.deltaTime;

        //Vertical rotation
        verticalRotation -= verticalMov;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Horizontal rotation
        player.transform.Rotate(Vector3.up * horizontalMov);
    }

    #endregion
}