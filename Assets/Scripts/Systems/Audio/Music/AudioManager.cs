using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static System.TimeZoneInfo;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource MusicMain;
    [SerializeField] private AudioSource MusicBlend;
    [SerializeField] public List<AudioClip> MusicClips = new List<AudioClip>();

    public float defaultVolume = 0.7f;
    public float transitionTime = 0.5f;

    private void OnEnable()
    {
        SceneAudioTrigger.SceneMusicTrigger += ChangeMusicToGivenClip;
    }
    private void OnDisable()
    {
        SceneAudioTrigger.SceneMusicTrigger -= ChangeMusicToGivenClip;
    }
    public void ChangeMusicToGivenClip(int AudioClipNumbInList)
    {
        if(MusicMain.isPlaying)
        {
            AudioSource Now = MusicMain;
            AudioSource Shift = MusicBlend;
            Shift.clip = MusicClips[AudioClipNumbInList];
            StartCoroutine(MixMusicAudios(Now, Shift));
        }
        else
        {
            AudioSource Shift = MusicMain;
            AudioSource Now = MusicBlend;
            Shift.clip = MusicClips[AudioClipNumbInList];
            StartCoroutine(MixMusicAudios(Now, Shift));
        }
    }
    IEnumerator MixMusicAudios(AudioSource Now, AudioSource Shift)
    {
        float percent = 0;
        while (Now.volume > 0)
        {
            Now.volume = Mathf.Lerp(defaultVolume, 0, percent);
            percent += Time.unscaledDeltaTime / transitionTime;
            yield return null;
        }

        Now.Pause();
        if (Shift.isPlaying == false)
        {
            Shift.Play();
        }
        Shift.UnPause();
        percent = 0;

        while (Shift.volume < defaultVolume)
        {
            Shift.volume = Mathf.Lerp(0, defaultVolume, percent);
            percent += Time.unscaledDeltaTime / transitionTime;
            yield return null;
        }
    }
}
