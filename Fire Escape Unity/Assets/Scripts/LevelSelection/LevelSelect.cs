// Author: Jacob Biles
/* Purpose & How to use
* Purpose: Loads a level based on scene name.
* How to use:
* Attach to canvas/panel/etc within ui
* Link button "OnClick" action, linking this panel or canvas to said button
* Set behavior to this script, and select "OpenLevel" function.
* Pass Level/Scene name within the field.
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Opens a level based on given name of scene
    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name);
        Debug.Log("Loading: " + name);
    }
}
