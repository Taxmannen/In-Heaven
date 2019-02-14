using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PatternPatternShotData))]
public class PatternPatternShotEditor : Editor
{
    List<GameObject> sceneGizmos = new List<GameObject>();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PatternPatternShotData data = target as PatternPatternShotData;


        foreach (var item in sceneGizmos)
        {
            DestroyImmediate(item);
        }
        //sceneGizmos.Clear();
        foreach (PatternShotData item in data.patternShotDatas)
        {
            List<PatternStruct> l = item.patternPrefab.GetComponent<PatternImporter>().patternList;
            foreach (var item2 in l)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(item2.x, item2.y, 0);
                sceneGizmos.Add(sphere);
            }
                
        }

        
    }
    private void OnDisable()
    {
        foreach (var item in sceneGizmos)
        {
            DestroyImmediate(item);
        }
    }
    private void OnSceneGUI()
    {
        
        
    }
}
