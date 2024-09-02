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
    private Animator cameraAnim;

    #endregion



    #region Methods

    private void OnEnable()
    {
        pInput = new InputSystem();
        pInput.Enable();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }



    private void Start()
    {
        cameraAnim = transform.Find("CameraParent").transform.Find("Camera").GetComponent<Animator>();
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

        //Normalizes movement for any direction - needed for camera rotation
        direction = transform.right * direction.x + transform.forward * direction.z;
        if(direction != Vector3.zero)
        {
            cameraAnim.SetBool("moving", true);
        }
        else
        {
            cameraAnim.SetBool("moving", false);
        }

        rb.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
    }

    #endregion

}

/* Refrences:
 * https://www.youtube.com/watch?v=LqnPeqoJRFY
 * Utilized physic based movement and considered other properties in video.
 * Decided agaisnt implementing drag since Rigidbody component already has options for drag.
 */