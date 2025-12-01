using UnityEngine;

public class HoseP2SpotScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael

    [SerializeField]
    private GameObject hoseNozzle;
    [SerializeField]
    private float percentagePerSecond;
    private Vector3 originalPos;
    private int modifier;
    private HoseNozzleScript hoseScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPos = transform.position;
        hoseScript = hoseNozzle.GetComponent<HoseNozzleScript>();
        modifier = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((hoseScript.CurrentPercentage > 0 && modifier < 0) || (hoseScript.CurrentPercentage < 1 && modifier > 0))
        {     
            hoseScript.CurrentPercentage += ((percentagePerSecond / 100) * Time.deltaTime) * modifier;
        }

        if (transform.parent == null)
        {
            transform.position = originalPos;
        }
    }

    public void SwapModifier()
    {
        modifier = -modifier;
    }
}
