using UnityEngine;

public class ReloadSoundSettings : MonoBehaviour
{
    [SerializeField] MasterVolumeManager masterVolume;
    [SerializeField] MusicVolumeManager musicVolume;
    [SerializeField] SFXVolumeManager sfxVolume;

    private void Awake()
    {
        masterVolume.LoadMasterSettings();
        musicVolume.LoadMusicSettings();
        sfxVolume.LoadSFXSettings();
    }
}
