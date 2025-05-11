using UnityEngine;
using UnityEngine.UI;
public class ImageUIHandlerScript : MonoBehaviour
{
    public Image Player1ItemImage;
    public Image Player2ItemImage;

    private void OnEnable()
    {
        ItemEventsScript.OnItemChanged += UpdateItemUI;
    }

    private void OnDisable()
    {
        ItemEventsScript.OnItemChanged -= UpdateItemUI;
    }

    private void UpdateItemUI(int PlayerID, Sprite? ItemSprite)
    {
        if (PlayerID == 1)
        {
            Player1ItemImage.sprite = ItemSprite;
            Player1ItemImage.enabled = ItemSprite != null;
        }
        else if (PlayerID == 2)
        {
            Player2ItemImage.sprite = ItemSprite;
            Player2ItemImage.enabled = ItemSprite != null;
        }
    }
}
