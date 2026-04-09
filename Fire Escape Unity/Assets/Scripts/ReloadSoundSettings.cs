using UnityEngine;


//Created by Henry Cummings
public class ReloadSoundSettings : MonoBehaviour
{
    [SerializeField] MasterVolumeManager masterVolume;
    [SerializeField] MusicVolumeManager musicVolume;
    [SerializeField] SFXVolumeManager sfxVolume;

    private void Start()
    {
        masterVolume.LoadMasterSettings();
        musicVolume.LoadMusicSettings();
        sfxVolume.LoadSFXSettings();
    }
}
