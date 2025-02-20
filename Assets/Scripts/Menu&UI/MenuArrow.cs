using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuArrow : Selectable
{
    [SerializeField] private Image arrow;

	private void Update()
	{
		if (IsHighlighted())
		{
			arrow.enabled = true;
		}
		else
		{
			arrow.enabled = false;
		}
	}
}
