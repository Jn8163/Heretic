using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	public HealthSystem healthSystem;

	[SerializeField]
	private int pLife, pAmmo, pArmor, pItemCount; // these are temporary until the systems for these are actually implemented

	public TextMeshProUGUI life, ammo, armor, itemCount;

	[SerializeField]
	private Image ammoIcon, itemIcon, yKey, gKey, bKey; 

	[SerializeField]
	private GameObject hpIndicator, lerpPointA, lerpPointB;

	private void Start()
	{
		KeyYPickupGA.KeyYPickup += KeyYellowLight;
		KeyGPickupGA.KeyGPickup += KeyGreenLight;
		KeyBPickupGA.KeyBPickup += KeyBlueLight;
	}

	private void Update()
	{
		life.text = healthSystem.currentHealth.ToString();
		ammo.text = pAmmo.ToString();
		armor.text = pArmor.ToString();
		if (pItemCount > 1)
		{
			itemCount.text = pItemCount.ToString();
		}
		else
		{
			itemCount.text = "";
		}

		// indicator changes depending on player hp, the more full their hp is the farther right the indicator goes

		// KEY ICONS: Use game action trigger script alongside a special key script to call delegate functions here. Change the image color of a key when its function is called.


	}

	private void FixedUpdate()
	{
		hpIndicator.transform.position = Vector2.Lerp(lerpPointA.transform.position, lerpPointB.transform.position, healthSystem.currentHealth / 100f);
	}

	private void KeyYellowLight(bool b)
	{
		yKey.color = Color.yellow;
	}

	private void KeyGreenLight(bool b)
	{
		gKey.color = Color.green;
	}

	private void KeyBlueLight(bool b)
	{
		bKey.color = Color.blue;
	}

	// AMMO ICON/COUNT: Have input action functions that pull up the ammo icon on the UI for the respective weapon
	// Ex: Pressing 2 would pull up the ammo icon for the crystal wand, as well as its current ammo
	// Make sure each input action function has its own respective icon and ammo count regarding the weapon
	// If using LB/RB or something similar to cycle weapons on controller, use an index for a switch case or if statement. Decrement/increment index accordingly
}
