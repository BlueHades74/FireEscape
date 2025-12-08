using UnityEditor;
using UnityEngine;

public class PlayerPrefReset : MonoBehaviour
{
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs Reset");
        }
    }
}
