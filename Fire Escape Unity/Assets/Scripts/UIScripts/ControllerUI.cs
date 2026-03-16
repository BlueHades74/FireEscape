using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerUI : MonoBehaviour
{
    [SerializeField]
    private EventSystem es;
    [SerializeField]
    private Selectable elementToSelect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        es = FindAnyObjectByType(typeof(EventSystem)) as EventSystem;
    }

    public void MoveToElement()
    {
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(elementToSelect.gameObject);
    }
}
