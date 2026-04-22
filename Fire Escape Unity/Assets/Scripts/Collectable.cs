using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickupSound;
    [SerializeField]
    private AudioSource playerAudioSource;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerAudioSource = collision.GetComponent<AudioSource>();
            playerAudioSource.clip = pickupSound;
            playerAudioSource.Play();
            Destroy(gameObject);
            BonusTracker.RegisterBonusCollected();
        }
    }
}
