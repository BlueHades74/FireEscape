using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited By: Rafael Gonzalez Atiles

    private Vector2 linearVelocity;

    [SerializeField]
    private float modifier;

    [SerializeField]
    private GameObject player;

    private Image image;

    private GameObject stairs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeOpacity();
        if (modifier > 0 && image.color.a >= 1)
        {
            modifier = -modifier;
            stairs.GetComponent<StaircaseScript>().TriggerTeleport();
        }
        else if (modifier < 0 && image.color.a <= 0)
        {
            modifier = -modifier;
            stairs.GetComponent<StaircaseScript>().FixCamera();
            //player.GetComponent<Rigidbody2D>().linearVelocity = linearVelocity;
            this.gameObject.SetActive(false);
        }
    }

    private void ChangeOpacity()
    {
        Color color = GetComponent<Image>().color;
        color.a += modifier;
        image.color = color;
    }

    public void ReceiveLinVelocityData(GameObject sender, Vector2 passedVelocity)
    {
        stairs = sender;
        linearVelocity = passedVelocity;
    }
}
