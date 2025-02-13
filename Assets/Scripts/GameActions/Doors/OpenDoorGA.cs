using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoorGA : MonoBehaviour
{
	private PlayerInput pInput;
	private bool isDoorOpen;
	private bool isInteractable;

	[SerializeField]
	private Animator anim;
	[SerializeField]
	private AudioSource audioSource;
	private void OnEnable()
	{
		pInput = new PlayerInput();
		pInput.Enable();

		pInput.Player.Interact.performed += OpenDoor;
	}

	private void OnDisable()
	{
		pInput.Player.Interact.performed -= OpenDoor;
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
			if (isDoorOpen)
				StartCoroutine(nameof(DoorTimer));
		}
	}

	private void OpenDoor(InputAction.CallbackContext c)
	{
		if (isInteractable)
		{
			if (!isDoorOpen)
			{
                audioSource.Play();
            }
            isDoorOpen = true;
			anim.SetBool("isDoorOpen", isDoorOpen);
		}
	}

	IEnumerator DoorTimer()
	{
		Debug.Log("Starting Timer");
		yield return new WaitForSeconds(5);
		CloseDoor();
	}

	private void CloseDoor()
	{
		isDoorOpen = false;
		anim.SetBool("isDoorOpen", isDoorOpen);
	}
}
