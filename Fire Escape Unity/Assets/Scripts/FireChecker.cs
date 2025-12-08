using UnityEditor.UI;
using UnityEngine;

public class FireChecker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Fire") == null)
        {
            this.gameObject.SetActive(false);
        }
    }
}
