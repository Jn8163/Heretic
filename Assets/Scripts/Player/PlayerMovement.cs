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

    private PlayerInput pInput;
    private Rigidbody rb;
    private Animator cameraAnim;

    private Vector3 direction, targetPos;
    private bool snapToPos;



    [Header("Auto-Step Field(s)")]

    [Tooltip("The max vertical length of step the player will automatically take.")]
    [SerializeField] private float stepHeight = .3f;

    private GameObject stepRayLower, stepRayUpper;

    #endregion



    #region Methods

    private void Awake()
    {
        cameraAnim = transform.Find("CameraParent").transform.Find("Camera").GetComponent<Animator>();
        stepRayLower = transform.Find("StepRayLower").gameObject;
        stepRayUpper = transform.Find("StepRayUpper").gameObject;
        rb = GetComponent<Rigidbody>();

        stepRayUpper.transform.localPosition = new Vector3(0, stepHeight, 0);
    }



    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

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



    private void LateUpdate()
    {
        if(direction.x != 0 && direction.z != 0)
        {
            if (animateCam)
            {
                cameraAnim.SetBool("moving", true);
            }
        }
        else
        {
            cameraAnim.SetBool("moving", false);
        }
    }   //Update animations



    public void TargetPosition(Vector3 position)
    {
        Debug.Log("Impleement Update Position");
        snapToPos = true;
        targetPos = position;
    }



    /// <summary>
    /// Movement function for physics based player movememnt.
    /// </summary>
    private void Move()
    {
        if (!snapToPos)
        {
            //get direction from input.
            direction.x = pInput.Player.Move.ReadValue<Vector2>().x;
            direction.z = pInput.Player.Move.ReadValue<Vector2>().y;



            //allign direction to current player allignment
            direction = transform.right * direction.x + transform.forward * direction.z;



            if (direction.x != 0 && direction.z != 0)//if player is moving
            {
                rb.linearVelocity = (direction * speed) + new Vector3(0, rb.linearVelocity.y, 0);

                AutoStep();
            }
        }
        else
        {
            snapToPos = false;
            rb.position = targetPos;
        }
    }



    /// <summary>
    /// Snaps the player to the top of a stepable ledge.
    /// </summary>
    private void AutoStep()
    {
        Debug.DrawRay(stepRayLower.transform.position, direction);
        if (Physics.BoxCast(stepRayLower.transform.position, new Vector3(.4f, .5f, .4f), direction, out RaycastHit hit, Quaternion.identity, 1f) && hit.transform.CompareTag("Ground"))
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

                //Update rb position instead of object since rb is being used for movement.
                rb.position = rb.position + new Vector3(0f, objectHeight, 0f);
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