using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IncrementScene : MonoBehaviour
{
    private PlayerInput pInput;
    private bool isInteractable;

	[SerializeField]
	private int sceneNumber;

	private void Start()
	{
		pInput = new PlayerInput();
		pInput.Enable();

		pInput.Player.Interact.performed += SwitchScene;
	}

	private void OnDisable()
	{
		pInput.Player.Interact.performed -= SwitchScene;
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

	private void SwitchScene(InputAction.CallbackContext c)
	{
		if (isInteractable)
		{
			SceneManager.LoadScene(sceneNumber);
		}
	}
}
