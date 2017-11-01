using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CommandLineCore))]
public class CommandLineCoreEditor : Editor {

    private Texture2D logo;

    public override void OnInspectorGUI()
    {
        logo = (Texture2D)Resources.Load("Textures/Logo", typeof(Texture2D));
        GUILayout.Label(logo);
        DrawDefaultInspector();
    }
}
