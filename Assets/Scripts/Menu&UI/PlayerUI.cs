using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour, IManageData
{
	public HealthSystem healthSystem;
	public ArmorSystem armorSystem;

	[SerializeField]
	private int pLife, pArmor, pItemCount; // these are temporary until the systems for these are actually implemented

	public TextMeshProUGUI life, ammo, armor, itemCount, announcementText;

	[SerializeField]
	private GameObject yKey, gKey, bKey;
	public bool yKeyObt, gKeyObt, bKeyObt;

	[SerializeField] private List<GameObject> ammoDisplays = new List<GameObject>();

	[SerializeField]
	private GameObject hpIndicator, lerpPointA, lerpPointB;

	private float tmp, difference;

	private bool adjustingHPBar;
	private bool wait;

	private bool setTMP;

	public AnimationCurve curve;
	public float shakeDuration = 1f;


	private void Awake()
	{
		yKey.SetActive(false);
		gKey.SetActive(false);
		bKey.SetActive(false);
	}
	private void Start()
	{
		tmp = healthSystem.currentHealth;
	}



    private void OnEnable()
    {
        KeyYPickupGA.KeyYPickup += KeyYellowLight;
        KeyGPickupGA.KeyGPickup += KeyGreenLight;
        KeyBPickupGA.KeyBPickup += KeyBlueLight;

        OpenKeyDoor.DisplayText += DisplayText;

		AmmoSystem.UpdateAmmoUI += UpdateAmmoDisplay;
		Weapon.MeleeWeaponActive += DisableAmmoDisplays;
    }



    private void OnDisable()
	{
		KeyYPickupGA.KeyYPickup -= KeyYellowLight;
		KeyGPickupGA.KeyGPickup -= KeyGreenLight;
		KeyBPickupGA.KeyBPickup -= KeyBlueLight;

		OpenKeyDoor.DisplayText -= DisplayText;

        AmmoSystem.UpdateAmmoUI -= UpdateAmmoDisplay;
        Weapon.MeleeWeaponActive -= DisableAmmoDisplays;
    }

    private void Update()
	{
		life.text = healthSystem.currentHealth.ToString();
		armor.text = armorSystem.currentShieldHealth.ToString();

		// indicator changes depending on player hp, the more full their hp is the farther right the indicator goes

		// KEY ICONS: Use game action trigger script alongside a special key script to call delegate functions here. Change the image color of a key when its function is called.


	}

	private void FixedUpdate()
	{
		if (!setTMP)
		{
			tmp = healthSystem.currentHealth;
			setTMP = true;
		}
		if (tmp != healthSystem.currentHealth)
		{
			if (!adjustingHPBar)
			{
				adjustingHPBar = true;
				StartCoroutine(nameof(GradualHP));
				// StartCoroutine(nameof(HealthBarShaking));
			}
		}
		hpIndicator.transform.position = Vector2.Lerp(lerpPointA.transform.position, lerpPointB.transform.position, tmp / 100f);
	}

	IEnumerator GradualHP()
	{
		yield return new WaitForSeconds(1 / 24);

		if (tmp > healthSystem.currentHealth)
		{
			tmp -= 0.5f;
		}
		else if (tmp < healthSystem.currentHealth)
		{
			tmp += 0.5f;
		}

		adjustingHPBar = false;
	}

	IEnumerator HealthBarShaking()
	{
		Vector2 startPosition1 = lerpPointA.transform.position;
		Vector2 startPosition2 = lerpPointA.transform.position;
		Vector2 tmpPosition = new Vector2(0, 0);
		float elapsedTime = 0f;

		while (elapsedTime < shakeDuration)
		{
			elapsedTime += Time.deltaTime;
			float strength = curve.Evaluate(elapsedTime / shakeDuration);
			tmpPosition.y = Random.insideUnitCircle.y;
			lerpPointA.transform.position = startPosition1 + tmpPosition;
			lerpPointB.transform.position = startPosition2 + tmpPosition;
			yield return null;
		}

		lerpPointA.transform.position = startPosition1;
		lerpPointB.transform.position = startPosition2;
	}
	
	private void KeyYellowLight(bool b)
	{
		yKey.SetActive(b);
		yKeyObt = b;
	}

	private void KeyGreenLight(bool b)
	{
		gKey.SetActive(b);
		gKeyObt = b;
    }

	private void KeyBlueLight(bool b)
	{
		bKey.SetActive(b);
		bKeyObt = b;
    }

	private void DisplayText(int index)
	{
		announcementText.text = "You need a key to open this door.";
		StartCoroutine(nameof(TextTimer));
	}



    private void DisableAmmoDisplays()
    {
        foreach (GameObject ammoDisplay in ammoDisplays)
        {
            ammoDisplay.SetActive(false);
        }
    }



    private void UpdateAmmoDisplay(Ammo ammoToDisplay, int ammoAmount)
	{
		DisableAmmoDisplays();

		if((int)ammoToDisplay < ammoDisplays.Count && (int)ammoToDisplay >= 0)
		{
			ammoDisplays[(int)ammoToDisplay].SetActive(true);
            ammoDisplays[(int)ammoToDisplay].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ammoAmount.ToString();
        }
    }



	private void ChangeAmmoDisplay(Ammo ammoToDisplay)
	{
		DisableAmmoDisplays();

        if ((int)ammoToDisplay < ammoDisplays.Count && (int)ammoToDisplay >= 0)
        {
            ammoDisplays[(int)ammoToDisplay].SetActive(true);
        }
    }



	IEnumerator TextTimer()
	{
		yield return new WaitForSeconds(5);

		announcementText.text = "";
	}

    public void LoadData(GameData data)
    {
		KeyYellowLight(data.yKey);
		KeyGreenLight(data.gKey);
		KeyBlueLight(data.bKey);
    }

    public void SaveData(ref GameData data)
    {
		data.yKey = yKeyObt;
		data.gKey = gKeyObt;
		data.bKey = bKeyObt;
    }

    // AMMO ICON/COUNT: Have input action functions that pull up the ammo icon on the UI for the respective weapon
    // Ex: Pressing 2 would pull up the ammo icon for the crystal wand, as well as its current ammo
    // Make sure each input action function has its own respective icon and ammo count regarding the weapon
    // If using LB/RB or something similar to cycle weapons on controller, use an index for a switch case or if statement. Decrement/increment index accordingly
}
