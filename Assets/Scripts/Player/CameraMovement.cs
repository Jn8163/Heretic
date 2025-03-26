using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject player;
    [SerializeField] private float sensitivity = 15f, gamepadSensitivity = 150f;
    [SerializeField] private LayerMask aimAssist;
    [SerializeField] private bool aimAssistActive;
    [SerializeField] private Rigidbody rb;

    private PlayerInput pInput;
    private float verticalRotation;
    private bool gamepadActive;

    private Vector3 enemyPos;

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



    private void FixedUpdate()
    {
        AimAssistRaycast();
        Rotation();
        AimAssistMove();
		
	}

	private void Update()
	{
		Debug.DrawLine(enemyPos, transform.position, Color.blue);
	}

	private void AimAssistRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50, aimAssist))
        {
            aimAssistActive = true;
        }
        else
        {
            aimAssistActive = false;
        }
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

    private void AimAssistMove()
    {

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 50, aimAssist))
		{
			aimAssistActive = true;
		}
		else
		{
			aimAssistActive = false;
		}

		if (aimAssistActive && gamepadActive)
        {
            // find enemy parent of aim assist layer
            enemyPos = hit.collider.GetComponentInParent<Transform>().position;
            // find vector3 direction from player to enemy
            Vector3 targetPos = enemyPos - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPos);
            Vector3 currentRotation = player.transform.rotation.eulerAngles;
            Vector3 targetRotationEuler = targetRotation.eulerAngles;
            targetRotationEuler.z = 0;
            targetRotation = Quaternion.Euler(targetRotationEuler);

            // use Quaternion.RotateTowards
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, targetRotation, 10 * Time.deltaTime);

            Vector3 tmp = player.transform.rotation.eulerAngles;
            tmp.z = 0;
            tmp.x = 0;
            player.transform.rotation = Quaternion.Euler(tmp);

		}
    }

    private void ControllerConnected(bool b)
    {
        gamepadActive = b;
    }

    #endregion
}