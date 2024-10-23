using System;
using System.Collections;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool bPreserve;

    private void Awake()
    {
        if (bPreserve)
            DontDestroyOnLoad(audioSource);
    }
    private void Start()
    {
        audioSource.Play();
    }
}
