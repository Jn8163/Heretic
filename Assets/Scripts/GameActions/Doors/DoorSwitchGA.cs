using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorSwitchGA : MonoBehaviour
{
    private PlayerInput pInput;
    private bool isButtonPressed;
    private bool isInteractable;

	//	public static Action OpenDoor = delegate { };

	[SerializeField]
	private Animator doorAnim, buttonAnim;
	[SerializeField]
	private AudioSource doorSound, buttonSound;

	private void Start()
	{
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Interact.performed += ButtonPressed;
	}

	private void OnDisable()
	{
        pInput.Player.Interact.performed -= ButtonPressed;
		pInput.Disable();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isInteractable = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isInteractable = false;
		}
	}

	private void ButtonPressed(InputAction.CallbackContext c)
	{
		if (isInteractable)
		{
			if (!isButtonPressed) {
                buttonSound.Play();
                doorSound.Play();
            }
            isButtonPressed = true;
            doorAnim.SetBool("isDoorOpen", isButtonPressed);
            buttonAnim.SetBool("isButtonPressed", isButtonPressed);

        }
    }
}
