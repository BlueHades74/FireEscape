using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
/// <summary>
/// This script allows for the altering of the music volume of the game through a slider and will correctly update the volume.
/// This script uses a logarithmic function to update the volume for the players as human hearing follows along a logarithmic process for how loud a sound/music is.
/// </summary>
public class MusicVolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private Slider musicVolumeSlider;

    private void Start()
    {
        //Setting up the slider and calling out to the audio mixer for when the volume is being changed by the player. Also loads saved Music volume settings.
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void SetMusicVolume(float volume)
    {
        //Adjusting music volume through a logarithmic function based on the value of the fill of the slider, and saving that preference for when the player later comes back to the game.
        musicAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        SaveMusicSettings(volume);
    }
    private void SaveMusicSettings(float volume)
    {
        //This function saves the player's music volume settings when called.
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    
}
