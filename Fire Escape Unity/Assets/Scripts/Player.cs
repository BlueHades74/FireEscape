using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FireChannel fireChannel = default;

    void OnEnable()
    {
        fireChannel.InteractEvent += OnInteract;
    }

    private void OnDisable()
    {
        fireChannel.InteractEvent -= OnInteract;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnInteract()
    {

    }
}
