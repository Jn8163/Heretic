using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CallStatsMenu : MonoBehaviour
{
	private PlayerInput pInput;
	private bool isInCollider;
	[SerializeField] private AudioSource aSource;

	public static Action<string> CallStats = delegate { };

	private void Start()
	{
		pInput = new PlayerInput();
		pInput.Enable();
		pInput.Player.Interact.performed += PushButton;
	}

	private void OnDisable()
	{
		pInput.Disable();
		pInput.Player.Interact.performed -= PushButton;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
			isInCollider = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
			isInCollider = false;
	}

	private void PushButton(InputAction.CallbackContext c)
	{
		if (isInCollider)
		{
			CallStats("StatsMenu");
			aSource.Play();
			GetComponent<Collider>().enabled = false;
			isInCollider = false;
			LevelEndResults.testBool = true;
		}
	}

	private void Update()
	{
		StatTracker.time = (int)Time.time;
	}
}
