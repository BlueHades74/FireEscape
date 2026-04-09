using UnityEngine;
using UnityEngine.SceneManagement;

//Created by Henry Cummings
public class BGMMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsToPlay;
    [SerializeField] private AudioClip timeRunningOutClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource fireAudioSource;
    private string _currentScene;
    private int _chosenTrack;
    [SerializeField] private Timer _timerScript;
    private bool _canChangeAudio = true;

    private void Start()
    {
        _chosenTrack = Random.Range(1, audioClipsToPlay.Length);
        _currentScene = SceneManager.GetActiveScene().name;
        if(_currentScene == "Firehouse")
        {
            audioSource.clip = audioClipsToPlay[0];
            fireAudioSource.clip = null;
        }
        else
        { 
            audioSource.clip = audioClipsToPlay[_chosenTrack];
            audioSource.Play();
        }
    }
    private void Update()
    {
        if(_timerScript != null)
        {
            if (_timerScript.currentTime <= 10 && _canChangeAudio == true)
            {
                ChangeMusic();
            }
        }
        else
        {
            return;
        }
    }

    private void ChangeMusic()
    {
        _canChangeAudio = false;
        audioSource.clip = timeRunningOutClip;
        audioSource.Play();
        audioSource.loop = false;
    }
}
