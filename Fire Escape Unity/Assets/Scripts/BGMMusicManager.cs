using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsToPlay;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource fireAudioSource;
    private string _currentScene;
    private int _chosenTrack;

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
}
