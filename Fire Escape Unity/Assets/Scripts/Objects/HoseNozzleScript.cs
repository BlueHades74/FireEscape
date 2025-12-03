using UnityEngine;

public class HoseNozzleScript : MonoBehaviour
{
    //Creater by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    [SerializeField]
    private float waterSprayDistance;
    [SerializeField]
    private float waterSprayWidth;
    [SerializeField]
    private float waterPushbackStrength;

    private float currentDistance;
    private float currentWidth;
    private float currentPushback;
    [SerializeField]
    private float currentPercentage;

    private GameObject waterSpray;

    public float CurrentPushback { get => currentPushback; }
    public float CurrentPercentage { get => currentPercentage; set => currentPercentage = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterSpray = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = waterSprayDistance * currentPercentage;
        currentWidth = waterSprayWidth * currentPercentage;
        currentPushback = waterPushbackStrength * currentPercentage;

        waterSpray.transform.localScale = new Vector3(currentWidth, currentDistance, 1);
    }
    
    public void SetRotation(Vector2 facingDirection)
    {
        float tempRotation = 0;
        if (facingDirection.x < 0)
        {
             tempRotation = 90;
        }
        else if (facingDirection.x > 0)
        {
            tempRotation = 270;
        }
        else if (facingDirection.y < 0)
        {
            tempRotation = 180;
        }
        else if (facingDirection.y > 0)
        {
            tempRotation = 0;
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, tempRotation, 0.1f));
    }
}
