using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GuiManager))]
public class GuiManagerHelper : Editor
{
    public override void OnInspectorGUI () {

        DrawDefaultInspector();
        GuiManager guiManager = (GuiManager) target;

        if (GUILayout.Button("Enable all Sections")) {
            guiManager.EnableAllSections();
        }

        if (GUILayout.Button("Disable all Sections")) {
            guiManager.DisableAllSections();
        }

    }
}
