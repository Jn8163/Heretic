using System;
using UnityEngine;

public class GraphicsSwapDelegate : MonoBehaviour
{
    [SerializeField] private GameObject _spriteObj;
    [SerializeField] private GameObject _meshObj;
    public static bool spriteInactive;

    private void OnEnable()
    {
		AssetSwapEntities.SwapGraphics += Swap;
    }

    private void Start()
	{
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

		spriteInactive = toggle;
	}

	public void ToggleVisibility(bool toggle)
	{
		Swap(toggle);
	}
}
