//Created by Sebastian Rosul on 04_08_2026 
//The purpose is so when the mouse hovers over the tool icons, it gives a brief description of what each tool is 
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public GameObject ToolDescriptor;
    private void Start()
    {
        ToolDescriptor.SetActive(false); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolDescriptor.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolDescriptor.SetActive(false);
    }
}
