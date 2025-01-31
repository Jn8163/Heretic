using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BagPickup : ImmediatePickup
{
	private AmmoSystem ammoSystem;

	[SerializeField] private List<int> ammoValues;
	private void OnEnable()
	{
		ammoSystem = FindAnyObjectByType<AmmoSystem>();

		if (MenuSystem.instance.selectedDifficulty == 1 || MenuSystem.instance.selectedDifficulty == 5)
		{
			for (int i = 0; i < ammoValues.Count; i++)
			{
				ammoValues[i] += ammoValues[i] / 2;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PickupItem();
		}
	}

	protected override void PickupItem()
	{
		base.PickupItem();
		ammoSystem.UpdateAmmoMax(ammoValues);
	}
}
