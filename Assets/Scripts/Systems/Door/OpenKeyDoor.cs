using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenKeyDoor : MonoBehaviour, IManageData
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
		if(isInteractable)
        {
            CheckToOpenDoor();
        }
	}

	private void CheckToOpenDoor()
	{
        PlayerUI pUI = FindAnyObjectByType<PlayerUI>();
        isKeyY = pUI.yKeyObt;
        isKeyG = pUI.gKeyObt;
        isKeyB = pUI.bKeyObt;
        if (index == 0 && isKeyY)
        {
            OpeningDoor();
        }
        else if (index == 1 && isKeyG)
        {
            OpeningDoor();
        }
        else if (index == 2 && isKeyB)
        {
            OpeningDoor();
        }
        else if (index == 0 && !isKeyY)
        {
            DisplayText(index);
        }
        else if (index == 1 && !isKeyG)
        {
            DisplayText(index);
        }
        else if (index == 2 && !isKeyB)
        {
            DisplayText(index);
        }
    }

    private void OpeningDoor()
    {
        isDoorOpen = true;
        anim.SetBool("isDoorOpen", isDoorOpen);
        audioSource.Play();
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
