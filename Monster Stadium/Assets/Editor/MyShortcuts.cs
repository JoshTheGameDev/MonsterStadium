using UnityEditor;
using UnityEngine;

public class MyShortcuts : Editor
{
    [MenuItem("GameObject/ActiveToggle #F1")] //Shift + F1 = Toggle GameObject(s) on/off
    static void ToggleActivationSelection()
    {
        foreach (GameObject go in Selection.gameObjects)
            go.SetActive(!go.activeSelf);
    }
}