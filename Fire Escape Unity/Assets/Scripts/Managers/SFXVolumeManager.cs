using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
/// <summary>
/// This script allows for the altering of the sound effects volume of the through a slider and will correctly update the volume.
/// This script uses a logarithmic function to update the volume for the players as human hearing follows along a logarithmic process for how loud something is.
/// Created by Henry Cummings
/// </summary>
public class SFXVolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer sfxAudiomixer;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        //Setting up the slider and calling out to the audio mixer for when the volume is being changed by the player. Also loads saved SFX volume settings.
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void SetSFXVolume(float volume)
    {
        //Adjusting music volume through a logarithmic function based on the value of the fill of the slider, and saving that preference for when the player later comes back to the game.
        sfxAudiomixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        SaveSFXSettings(volume);
    }

    private void SaveSFXSettings(float volume)
    {
        //This function saves the player's sound effects volume settings when called.
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
    
    public void LoadSFXSettings()
    {
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            //Setting up a default setting for sound effects if no player changes made.
            SetSFXVolume(1.0f);
        }
        else
        {
            //Should load saved settings when called.
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }
}
