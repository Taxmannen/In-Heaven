using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Made By: Anton Lindkvist
/// </summary>
[CustomEditor(typeof(PatternImporter))]
public class PatternImporterGUI : Editor
{
    PatternImporter patternImporter;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (patternImporter == null) patternImporter = (PatternImporter)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Pattern"))
        {
            try
            {
                patternImporter.GeneratePattern();
            }
            catch (System.Exception)
            {
                Debug.LogError("No texture found!");
            }

        }
        GUILayout.EndHorizontal();
    }
}
