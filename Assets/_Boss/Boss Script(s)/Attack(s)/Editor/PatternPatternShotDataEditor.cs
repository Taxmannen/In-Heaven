using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PatternPatternShotData))]
public class PatternPatternShotEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Gizmos.DrawSphere(Vector3.zero, 1);
    }
    private void OnSceneGUI()
    {
        
        
    }
}
