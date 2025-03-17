using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]

/// <summary>
/// Player movement utilizing Unity Physics and new Input System.
/// </summary>
public class PlayerMovement : MonoBehaviour, IManageData
{

    #region Fields

    [Header("Movement Field(s)")]

    [SerializeField] private float walkSpeed = 15f;
    [SerializeField] private float runSpeed = 25f;
    [SerializeField] private bool animateCam = true;
    [SerializeField] private AudioSource footSteps, footStepsSprint, currentFootsteps;

    private PlayerInput pInput;
    private Rigidbody rb;
    private Animator cameraAnim;

    private float speed = 15f;
    private Vector3 direction, targetPos;
    private bool snapToPos, movementLock;
    private bool walking, sprinting;



    [Header("Auto-Step Field(s)")]

    [Tooltip("The max vertical length of step the player will automatically take.")]
    [SerializeField] private float stepHeight = .3f;

    private GameObject stepRayLower, stepRayUpper;

    #endregion



    #region Methods

    private void Awake()
    {
        speed = walkSpeed;
        currentFootsteps = footSteps;
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

        pInput.Player.Sprint.performed += Sprint;
        pInput.Player.Sprint.canceled += Sprint;
        rb.freezeRotation = true;
        movementLock = false;
    }



    private void OnDisable()
    {
        pInput.Player.Sprint.performed -= Sprint;
        pInput.Player.Sprint.canceled -= Sprint;
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



    public void PlayerMovementLocked(bool locked)
    {
        movementLock = locked;
    }



    /// <summary>
    /// Movement function for physics based player movememnt.
    /// </summary>
    private void Move()
    {
        if (!snapToPos && !movementLock)
        {
            //get direction from input.
            direction.x = pInput.Player.Move.ReadValue<Vector2>().x;
            direction.z = pInput.Player.Move.ReadValue<Vector2>().y;


            //allign direction to current player allignment
            direction = transform.right * direction.x + transform.forward * direction.z;



            if (direction.x != 0 && direction.z != 0)//if player is moving
            {
                rb.linearVelocity = (direction * speed) + new Vector3(0, rb.linearVelocity.y, 0);

                //play footstep sound effect when player starts walking
                if (footSteps && walking == false)
                {
                    //set walking to true so sound effect doesn't play multiple times
                    walking = true;
                    currentFootsteps.Play();
                }

                AutoStep();
            }
            else
            {
                //stop footstep sound effect when not moving
                if (footSteps)
                {
                    walking = false;
                    currentFootsteps.Stop();
                }
            }
        }
        else if(snapToPos)
        {
            snapToPos = false;
            targetPos.y = transform.position.y;
            rb.position = targetPos;
        }
    }

    private void Sprint(InputAction.CallbackContext Sprint)
    {
        if(sprinting)
        {
            currentFootsteps.Stop();
            sprinting = false;
            speed = walkSpeed;
            currentFootsteps = footSteps;
            currentFootsteps.Play();
        }
        else
        {
            currentFootsteps.Stop();
            currentFootsteps = footStepsSprint;
            sprinting = true;
            walking = false;
            speed = runSpeed;
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

    public void LoadData(GameData data)
    {
        if (data.playerPosition != Vector3.zero)
        {
            transform.position = data.playerPosition;
            transform.rotation = new Quaternion(data.playerRotation.x, data.playerRotation.y, data.playerRotation.z, data.playerRotation.w);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = transform.position;
        data.playerRotation = new Vector4(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
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