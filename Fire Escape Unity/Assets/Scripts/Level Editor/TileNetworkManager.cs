using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class TileNetworkManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Vector2Int networkSize;

    [SerializeField]
    private Vector2Int NetworkTileSize;

    [SerializeField]
    private int NetworkFloors = 1;

    [SerializeField]
    GameObject canvas;

    Material networkShader;
    Vector2 canvasScale;
    Vector2 networkScale;
    GameObject canvasParent;

    private void Start()
    {
        canvasParent = this.transform.parent.gameObject;
        networkShader = (this.gameObject.GetComponent<MeshRenderer>()).material;

        /*  Finds the currently used shader that creates the visual graph on the TIleNetwork's canvas.
         *  With that variable, we can read/change the shader's properties based on our own fields in this script.
         *  Note: DO NOT use the shader's own fields for size and scale, as they take floats, which can be unsafe when creating the tile network.
         *  There is no way to change the shader properties to support ints without the use of external tools.
         */

        Vector4 shaderScale = new Vector4(networkSize.x, networkSize.y); 
        Vector4 shaderSize = new Vector4(NetworkTileSize.x, NetworkTileSize.y);

        Debug.LogWarning(networkShader);
        if (networkShader != null)
        {
        }

        TileNetwork tileNetwork = new TileNetwork(networkSize.x, networkSize.y, NetworkFloors, NetworkTileSize);
        canvasParent.transform.position = Vector3.zero;

        canvasScale = new Vector2(networkSize.x * .1f, networkSize.y * .1f);
        canvasParent.transform.localScale = new Vector3(canvasScale.x, canvasScale.y, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
