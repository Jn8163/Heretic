using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenKeyDoor : MonoBehaviour
{
	private PlayerInput pInput;
	private bool isDoorOpen;
	private bool isInteractable;
	private bool isKeyY, isKeyG, isKeyB;

	public static Action<int> DisplayText = delegate { };

	[SerializeField]
	private Animator anim;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	[Range(0,2)]
	private int index; // 0 is yellow, 1 is green, 2 is blue
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
		}
	}
	private void OpenDoor(InputAction.CallbackContext c)
	{
		PlayerUI pUI = FindAnyObjectByType<PlayerUI>();
		isKeyY = pUI.yKeyObt;
		isKeyG = pUI.gKeyObt;
		isKeyB = pUI.bKeyObt;
		if (index == 0 && isKeyY)
		{
			if (isInteractable)
			{
				isDoorOpen = true;
				anim.SetBool("isDoorOpen", isDoorOpen);
                audioSource.Play();

            }
        }
		else if (index == 1 && isKeyG)
		{
			if (isInteractable)
			{
                isDoorOpen = true;
				anim.SetBool("isDoorOpen", isDoorOpen);
                audioSource.Play();
            }
        }
		else if (index == 2 && isKeyB)
		{
			if (isInteractable)
			{
                isDoorOpen = true;
				anim.SetBool("isDoorOpen", isDoorOpen);
                audioSource.Play();
            }
        }

		else if(index == 0 && !isKeyY)
		{
			if (isInteractable)
			{
				DisplayText(index);
			}
		}

		else if (index == 1 && !isKeyG)
		{
			if (isInteractable)
			{
				DisplayText(index);
			}
		}

		else if (index == 2 && !isKeyB)
		{
			if (isInteractable)
			{
				DisplayText(index);
			}
		}
	}
}
