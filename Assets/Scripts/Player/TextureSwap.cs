using System;
using UnityEngine;

public class TextureSwap : MonoBehaviour
{
    [SerializeField] private Material oldMat;
    [SerializeField] private Material newMat;

	private void Start()
	{
		AssetSwapEntities.SwapGraphics += Swap;
		GetComponent<MeshRenderer>().sharedMaterial = oldMat;
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
			GetComponent<MeshRenderer>().sharedMaterial = newMat;
		}
		else
		{
			GetComponent<MeshRenderer>().sharedMaterial = oldMat;
		}
	}
}
