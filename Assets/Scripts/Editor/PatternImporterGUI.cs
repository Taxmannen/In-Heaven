using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatternImporter))]
public class PatternImporterGUI : Editor
{
    PatternImporter patternImporter;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (patternImporter == null) patternImporter = (PatternImporter)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Map"))
        {
            patternImporter.Thing();
        }
        GUILayout.EndHorizontal();
    }
}
