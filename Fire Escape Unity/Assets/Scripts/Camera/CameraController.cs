using UnityEngine;
using UnityEngine.UI;

public class SplitScreenController : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;
    public Camera splitCamOne, splitCamTwo, playerOneMain, playerTwoMain;
    public RenderTexture playerOneRender, playerTwoRender;

    public RawImage targetImage;
    public float splitDifference;



    void Update()
    {

        float playerDifference = Vector2.Distance(transform.position, playerOne.transform.position) - Vector2.Distance(transform.position, playerTwo.transform.position);

        if (playerDifference > splitDifference)
        {
            splitCamOne.enabled = true;
            splitCamTwo.enabled = true;
            playerOneMain.targetTexture = playerOneRender;
            playerTwoMain.targetTexture = playerTwoRender;
        }
        else
        {
            splitCamOne.enabled = false;
            splitCamTwo.enabled = false;
            playerOneMain.targetTexture = null;
            playerTwoMain.targetTexture = null;
        }


    }
}
