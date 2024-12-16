using System;
using UnityEngine;

public class GraphicsSwapDelegate : MonoBehaviour
{
    [SerializeField] private GameObject _spriteObj;
    [SerializeField] private GameObject _meshObj;
	[HideInInspector] public bool spriteActive = true;

	private void Start()
	{
		AssetSwapEntities.SwapGraphics += Swap;
		_spriteObj.SetActive(true);
		_meshObj.SetActive(false);
	}

	private void OnDisable()
	{
		AssetSwapEntities.SwapGraphics -= Swap;
	}

	private void Swap(bool toggle)
	{
		// if toggle is true, turn on new graphics and disable old
		// if toggle is false, do the opposite
		if (toggle)
		{
			_spriteObj.SetActive(false);
			_meshObj.SetActive(true);
		}
		else
		{
			_spriteObj.SetActive(true);
			_meshObj.SetActive(false);
		}

		spriteActive = toggle;
	}

	public void ToggleVisibility(bool toggle)
	{
        _spriteObj.SetActive(false);
        _meshObj.SetActive(false);

		if (toggle)
		{
			if (spriteActive)
			{
				_spriteObj.SetActive(toggle);
			}
			else
			{
				_meshObj.SetActive(toggle);
			}
		}
	}
}
