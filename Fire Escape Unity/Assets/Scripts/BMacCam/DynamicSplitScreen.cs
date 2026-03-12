using UnityEngine;
using UnityEngine.UI;

public class DynamicSplitScreen : MonoBehaviour
{
    [SerializeField] private Camera cam1, cam2;
    [SerializeField] private Transform player1, player2;
    [SerializeField] private Material splitMaterial;
    [SerializeField] private RawImage outputDisplay;

    private RenderTexture rt1, rt2;

    void Start()
    {
        rt1 = new RenderTexture(Screen.width, Screen.height, 24);
        rt2 = new RenderTexture(Screen.width, Screen.height, 24);

        cam1.targetTexture = rt1;
        cam2.targetTexture = rt2;

        splitMaterial.SetTexture("_Player1Tex", rt1);
        splitMaterial.SetTexture("_Player2Tex", rt2);

        outputDisplay.texture = rt1;
        outputDisplay.material = splitMaterial;

        splitMaterial.mainTexture = rt1;
    }

    void Update()
    {
        Vector3 p1Screen = cam1.WorldToViewportPoint(player1.position);
        Vector3 p2Screen = cam1.WorldToViewportPoint(player2.position);

        Vector2 diff = new Vector2(p2Screen.x - p1Screen.x, p2Screen.y - p1Screen.y);

        // 1. Pass the direction (normalized)
        splitMaterial.SetVector("_SplitDirection", diff.normalized);

        // 2. Set a SMALL constant for the line sharpness (e.g., 0.02)
        // Don't use the 'distance' variable here!
        splitMaterial.SetFloat("_SplitDistance", 0.02f);

        // 3. Midpoint of the split
        splitMaterial.SetVector("_SplitOrigin", (p1Screen + p2Screen) * 0.5f);
    }
}