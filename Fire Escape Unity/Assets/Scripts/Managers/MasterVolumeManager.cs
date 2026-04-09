using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
/// <summary>
/// This script allows for the altering of the overall volume of the game through a slider and will correctly update the volume.
/// This script uses a logarithmic function to update the volume for the players as human hearing follows along a logarithmic process for how loud a sound/music is.
/// Created by Henry Cummings
/// </summary>
public class MasterVolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer masterAudioMixer;
    [SerializeField] private Slider masterVolumeSlider;

    private void Start()
    {
        //Setting up the slider and calling out to the audio mixer for when the volume is being changed by the player. Also loads saved Master volume settings.
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

    public void SetMasterVolume(float volume)
    {
        //Adjusting master volume through a logarithmic function based on the value of the fill of the slider, and saving that preference for when the player later comes back to the game.
        masterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        SaveMasterSettings(volume);
    }
    private void SaveMasterSettings(float volume)
    {
        //This function saves the player's master volume settings when called.
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void LoadMasterSettings()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            //Setting a default Master Volume if no changes made by player.
            SetMasterVolume(1.0f);
        }
        else
        {
            //Should load saved settings when called.
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        }
    }
}
