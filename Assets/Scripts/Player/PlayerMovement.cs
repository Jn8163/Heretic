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
    [SerializeField] private bool animateCam = true;
    [SerializeField]

    private InputSystem pInput;
    private Rigidbody rb;
    private Animator cameraAnim;

    private GameObject groundCheck;
    private bool grounded = true;

    private Vector3 direction;



    [Header("Auto-Step Field(s)")]

    [Tooltip("The max vertical length of step the player will automatically take.")]
    [SerializeField] private float stepHeight = .3f;

    [Tooltip("How smooth auto-step's will be: Smaller = smoother")]
    [SerializeField] private float stepRate = .1f;

    private GameObject stepRayLower, stepRayUpper;


    #endregion



    #region Methods

    private void Awake()
    {
        cameraAnim = transform.Find("CameraParent").transform.Find("Camera").GetComponent<Animator>();
        stepRayLower = transform.Find("StepRayLower").gameObject;
        stepRayUpper = transform.Find("StepRayUpper").gameObject;
        groundCheck = transform.Find("GroundCheck").gameObject;
        rb = GetComponent<Rigidbody>();

        stepRayUpper.transform.localPosition = new Vector3(0, stepHeight, 0);
    }



    private void OnEnable()
    {
        pInput = new InputSystem();
        pInput.Enable();

        rb.freezeRotation = true;
    }



    private void OnDisable()
    {
        pInput.Disable();
    }



    private void FixedUpdate()
    {
        //GroundCheck();
        Move();
    }

    
    //In progress
    private void GroundCheck()
    {
        Debug.DrawLine(groundCheck.transform.position, groundCheck.transform.position - new Vector3(0, .3f, 0), Color.red, .1f);
        if(Physics.BoxCast(groundCheck.transform.position, new Vector3(.5f,.01f,.5f), Vector3.down, out RaycastHit hit, Quaternion.identity, .5f))
        {
            if(!(hit.point.y + .1f < transform.position.y))
            {
                grounded = true;
            }
        }
        else
        {
            grounded = false;
        }
    }

    private void Move()
    {
        direction.x = pInput.Player.Move.ReadValue<Vector2>().x;
        direction.z = pInput.Player.Move.ReadValue<Vector2>().y;

        direction = transform.right * direction.x + transform.forward * direction.z;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            rb.AddForce( direction * speed, ForceMode.VelocityChange);

            if (animateCam)
            {
                cameraAnim.SetBool("moving", true);
            }

            AutoStep();
        }
        else
        {
            cameraAnim.SetBool("moving", false);
        }
    }



    private void AutoStep()
    {
        if (Physics.BoxCast(stepRayLower.transform.position, new Vector3(.1f,.9f, .1f), direction, Quaternion.identity, 1f))
        {
            if (!Physics.BoxCast(stepRayUpper.transform.position, new Vector3(.1f, .1f, .1f), direction, Quaternion.identity, 1f))
            {
                float boxCastHeightInc = .1f;
                float objectHeight = 0;
                while(Physics.BoxCast(stepRayLower.transform.position + new Vector3(0, boxCastHeightInc, 0), new Vector3(.1f, .1f, .1f), direction, Quaternion.identity, 1f))
                {
                    objectHeight = boxCastHeightInc;
                    boxCastHeightInc += .1f;
                }
                rb.position += new Vector3(0f, objectHeight, 0f);
            }
        }
    }

    #endregion
}

/* Refrences:
 * Basic Movement:
 * https://www.youtube.com/watch?v=LqnPeqoJRFY
 * Utilized physic based movement and considered other properties in video.
 * Decided agaisnt implementing drag since Rigidbody component already has options for drag.
 * 
 * Auto Step Up:
 * https://www.youtube.com/watch?v=DrFk5Q_IwG0&t=547s
 * Major rework of code was done, but basics we're found here.
 */