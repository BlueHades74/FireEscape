using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInventoryUIScript : MonoBehaviour
{
    public int PlayerID;
    private ItemUIScript CurrentItem;

    public void SetHeldItem(ItemUIScript NewItem)
    {
        Debug.Log("SetHeldItem called for Player " + PlayerID);
        CurrentItem = NewItem;
        Debug.Log("Picked up item" + CurrentItem.ItemName + " Sprite: " + CurrentItem.ItemSprite);
        ItemEventsScript.OnItemChanged?.Invoke(PlayerID, CurrentItem?.ItemSprite);
    }

    public void DropItem()
    {
        CurrentItem = null;
        ItemEventsScript.OnItemChanged.Invoke(PlayerID, null);
    }

}
