using UnityEngine;
using UnityEngine.InputSystem;

public class LockMapCameraRotation : MonoBehaviour
{
	private GameObject player;
	private PlayerInput pInput;
	private bool minusHeld, plusHeld;
	private void OnEnable()
	{
		player = GameObject.Find("Player");

		pInput = new PlayerInput();
		pInput.Enable();
		pInput.Player.MapZoomIn.performed += ZoomIn;
		pInput.Player.MapZoomOut.performed += ZoomOut;
		pInput.Player.MapZoomOut.canceled += ZoomOutCancel;
		pInput.Player.MapZoomIn.canceled += ZoomInCancel;
	}

	private void OnDisable()
	{
		pInput.Disable();
		pInput.Player.MapZoomIn.performed -= ZoomIn;
		pInput.Player.MapZoomOut.performed -= ZoomOut;
		pInput.Player.MapZoomOut.canceled -= ZoomOutCancel;
		pInput.Player.MapZoomIn.canceled -= ZoomInCancel;
	}
	private void Update()
	{
		Vector3 pos = transform.position;
		pos = player.transform.position;
		pos.y += 30;
		transform.position = pos;

		if (plusHeld)
		{
			GetComponent<Camera>().orthographicSize -= 0.5f;
		}

		if (minusHeld)
		{
			GetComponent<Camera>().orthographicSize += 0.5f;
		}

		GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, 50, 100);
	}

	private void ZoomIn(InputAction.CallbackContext c)
	{
		plusHeld = true;
	}

	private void ZoomOut(InputAction.CallbackContext c)
	{
		minusHeld = true;
	}

	private void ZoomOutCancel(InputAction.CallbackContext c)
	{
		minusHeld = false;
	}
	private void ZoomInCancel(InputAction.CallbackContext c)
	{
		plusHeld = false;
	}
}
