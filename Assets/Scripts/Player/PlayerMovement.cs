using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

/// <summary>
/// Player movement utilizing Unity Physics and new Input System.
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    #region Fields

    [Header("Movement Field(s)")]
    [SerializeField] private float speed = 6f;
    private InputSystem pInput;
    private Rigidbody rb;
    private Vector3 direction;

    #endregion



    #region Methods

    private void OnEnable()
    {
        pInput = new InputSystem();
        pInput.Enable();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }



    private void OnDisable()
    {
        pInput.Disable();
    }



    private void FixedUpdate()
    {
        Move();
    }



    private void Move()
    {
        direction.x = pInput.Player.Move.ReadValue<Vector2>().x;
        direction.y = 0;
        direction.z = pInput.Player.Move.ReadValue<Vector2>().y;

        rb.AddForce(direction.normalized * speed, ForceMode.Acceleration);
    }

    #endregion

}

/*
 * Resource Used:
 * https://www.youtube.com/watch?v=LqnPeqoJRFY
 * Utilized physic based movement and considered other properties in video.
 * Decided agaisnt implementing drag since Rigidbody component already has options for drag.
 */