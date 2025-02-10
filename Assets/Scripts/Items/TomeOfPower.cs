using System;
using System.Collections;
using UnityEngine;

public class TomeOfPower : InventoryItem
{
	/*public static Action SuperchargeWeapons = delegate { };
	public static Action UnchargeWeapons = delegate { };*/
	private GameObject player;
	public override void Action()
	{
		/*StartCoroutine(nameof(TomeUse));*/
		/*SuperchargeWeapons();*/
		player = GameObject.Find("Player");
		player.GetComponent<ActivateTome>().TomeUsed();
	}

	/*IEnumerator TomeUse()
	{
		*//*SuperchargeWeapons();
		Debug.Log("charged!");
		yield return new WaitForSeconds(5);
		UnchargeWeapons();
		Debug.Log("not charged");*//*
	}*/
}
