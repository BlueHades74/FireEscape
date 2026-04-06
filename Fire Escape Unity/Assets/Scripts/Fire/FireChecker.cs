using UnityEngine;

public class FireChecker : MonoBehaviour
{
    // Created by: Brian McLatchie
    // Last Edited by: Brian McLatchie

    private ParticleSystem smoke;
    private bool smokeOff = false;

    private void Start()
    {
        // Get the particle system component from the child
        smoke = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check how many objects have tag "Fire" and if smoke is already turned off
        if (GameObject.FindGameObjectsWithTag("Fire").Length == 0 && smokeOff == false)
        {
            // Set smoke emission to 0
            var smokeEm = smoke.emission;
            smokeEm.rateOverTimeMultiplier = 0;
            smokeOff = true;
        }
    }
}
