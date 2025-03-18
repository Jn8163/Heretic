using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoorGA : MonoBehaviour, IManageData
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
			OpeningDoor();
		}
	}

	private void OpeningDoor()
	{
        if (!isDoorOpen)
        {
            audioSource.Play();
        }
        isDoorOpen = true;
        anim.SetBool("isDoorOpen", isDoorOpen);
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

    public void LoadData(GameData data)
    {
        if (TryGetComponent<GameObjectID>(out GameObjectID goID))
        {
            string id = goID.GetID();
            if (data.doorsOpen.ContainsKey(id))
            {
                isDoorOpen = data.doorsOpen[id];
				if (isDoorOpen)
				{
					OpeningDoor();
				}
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (TryGetComponent<GameObjectID>(out GameObjectID goID))
        {
            string id = goID.GetID();
            if (data.doorsOpen.ContainsKey(id))
            {
                data.doorsOpen[id] = isDoorOpen;
            }
            else
            {
                data.doorsOpen.Add(id, isDoorOpen);
            }
        }
    }
}
