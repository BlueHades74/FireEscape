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
        // Chooses a track, and checks the current scene. If the current scene isn't the firehouse, then it'll run the chosen track. Otherwise it plays the firehouse song set aside for the firehouse.
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
        //Checking for the timer item, and should it be true it will switch the music when the timer runs low for the player.
        if(_timerScript != null)
        {
            //This prevents a looping changing of the music, while still adjusting the music for the time running out.
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
        //Sets the boolean state that determines if the music can change properly, adjusts the music and prevents it from looping on the ending of a level screen.
        _canChangeAudio = false;
        audioSource.clip = timeRunningOutClip;
        audioSource.Play();
        audioSource.loop = false;
    }
}
