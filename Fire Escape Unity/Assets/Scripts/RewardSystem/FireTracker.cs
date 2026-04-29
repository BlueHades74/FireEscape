using UnityEngine;

//Author Alex
public class FireTracker : MonoBehaviour
{
    public static FireTracker Instance;
    private int totalFireCount;
    public AudioSource BGMFireAudio;

    void Start()
    {
        totalFireCount = GameObject.FindGameObjectsWithTag("Fire").Length;
        Instance = this;
    }

    public float GetPercentExtinguished()
    {
        if (totalFireCount == 0) 
        {
            BGMFireAudio.GetComponent<AudioSource>().Pause();
            return 1f;
        }

        int activeFires = 0;

        foreach (var fire in GameObject.FindGameObjectsWithTag("Fire"))
        {
            if (fire.activeInHierarchy)
                activeFires++;
        }

        int extinguished = totalFireCount - activeFires;
        return (float)extinguished / totalFireCount;
    }
}
