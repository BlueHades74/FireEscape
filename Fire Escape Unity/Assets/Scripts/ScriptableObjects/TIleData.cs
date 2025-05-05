using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TIleData", menuName = "Scriptable Objects/TIleData")]
public class TIleData : ScriptableObject
{
    public TileBase[] tiles;

    public bool flammable; 
}
