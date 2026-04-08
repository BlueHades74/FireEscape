using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    public GameObject ToolDescriptor; 
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolDescriptor.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolDescriptor.SetActive(false);
    }
}
