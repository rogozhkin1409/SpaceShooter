using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BackgroundController))]
public class BackgroundControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("GenerateBackground"))
        {
            var background = target as BackgroundController;
            background.GenerateBackground();
        }
    }
}