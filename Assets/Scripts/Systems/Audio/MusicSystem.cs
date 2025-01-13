using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class MusicSystem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    //private bool destroy;
    //[SerializeField] private float fade_out = 1.0f;
    
    public static MusicSystem instance;

    public static Action<bool> MusicSystemActive = delegate { };
    public static Action<bool> MusicSystemDeactivate = delegate { };

    private void Awake()
    {
        if (!instance)
        {
            Debug.Log("Instance is null. Defining new instance");
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource.Play();
        }
        else if (instance != this)
        {
            if (audioSource)
                audioSource.Stop();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        audioSource.Play();
    }
    
    /*public void ActivateMusic(GameObject currentSong)
    {
        currentSong.GetComponent<AudioSource>().Play();
    }
    public void DeactivateMusic(GameObject currentSong)
    {
            currentSong.GetComponent<AudioSource>().Stop();
    }*/

    public void FadeOut()
    {
        //Fade out goes here
    }

    private void OnDestroy()
    {
      /*audioSource.Stop();*/
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene change");
        if (audioSource!= null)
            audioSource.Stop();
    }
}
