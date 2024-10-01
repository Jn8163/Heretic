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
    [SerializeField] private bool animateWeapon = true;
    [SerializeField]

    private PlayerInput pInput;
    private Rigidbody rb;
    private Animator cameraAnim;
    private Animator weaponAnim;

    private Vector3 direction;



    [Header("Auto-Step Field(s)")]

    [Tooltip("The max vertical length of step the player will automatically take.")]
    [SerializeField] private float stepHeight = .3f;

    private GameObject stepRayLower, stepRayUpper;


    #endregion



    #region Methods

    private void Awake()
    {
        cameraAnim = transform.Find("CameraParent").transform.Find("Camera").GetComponent<Animator>();
        weaponAnim = transform.Find("WeaponHolder").transform.Find("Wand").GetComponent <Animator>();
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



    private void Move()
    {
        //get direction from input.
        direction.x = pInput.Player.Move.ReadValue<Vector2>().x;
        direction.z = pInput.Player.Move.ReadValue<Vector2>().y;



        //allign direction to current player allignment
        direction = transform.right * direction.x + transform.forward * direction.z;



        if (direction != Vector3.zero)//if player is moving
        {
            if (Physics.SphereCast(transform.position, 0.35f, direction, out RaycastHit hit, 0.35f))
            {
                direction += hit.normal * (transform.position - hit.point).magnitude;
            }
            else if (Physics.SphereCast(transform.position, 0.35f, direction + Vector3.right * -.5f, out RaycastHit hitl, 0.35f))
            {
                direction += hitl.normal * (transform.position - hit.point).magnitude;
            }
            else if (Physics.SphereCast(transform.position, 0.35f, direction + Vector3.right * .5f, out RaycastHit hitr, 0.35f))
            {
                direction += hitr.normal * (transform.position - hit.point).magnitude;
            }

            direction.Normalize();



            rb.linearVelocity = (direction * speed) + new Vector3(0, rb.linearVelocity.y, 0);



            if (animateCam)
            {
                cameraAnim.SetBool("moving", true);
            }

            
            if (animateWeapon)
            {
                weaponAnim.SetBool("moving", true);
            }
            

            AutoStep();
        }
        else
        {
            cameraAnim.SetBool("moving", false);
            weaponAnim.SetBool("moving", false);
        }
    }



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