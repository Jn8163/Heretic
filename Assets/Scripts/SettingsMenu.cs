using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer Master;
    [SerializeField] private Slider MusicSlider;

    [SerializeField] private Slider SFXSlider;


    public void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            LoadMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            LoadSFXVolume();
        }

        MusicVolume();
        SFXVolume();
    }
    public void MusicVolume()
    {
        float volume = MusicSlider.value;
        Master.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music", volume);
    }

    private void LoadMusicVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("Music");

        MusicVolume();
    }

    public void SFXVolume()
    {
        float volume = SFXSlider.value;
        Master.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    private void LoadSFXVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFX");

        MusicVolume();
    }

}
