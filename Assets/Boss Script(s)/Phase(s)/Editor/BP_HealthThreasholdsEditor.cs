using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
[CustomEditor(typeof(BP_HealthThreasholds))]
public class BP_HealthThreasholdsEditor : Editor
{
    BP_HealthThreasholds bP;
    List<BP_HealthThreasholds.HealthRanges> healthRanges;

    //List<BossPhase> hr_Phases = new List<BossPhase>();
    //List<float> hr_Threashold = new List<float>();


    bool showHealthRanges;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        ListOfPhases();
        
    }
    private void OnEnable()
    {
        bP = target as BP_HealthThreasholds;
        bP.bossPhases = bP.bossPhases.OrderByDescending(x => x.threashhold).ToList();
        healthRanges = bP.bossPhases;
    }
    private void OnDestroy()
    {
        if(target != null)
        {
            healthRanges = healthRanges.OrderByDescending(x => x.threashhold).ToList();
            bP.bossPhases = healthRanges;
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
        
    }


    void ListOfPhases()
    {
        showHealthRanges = EditorGUILayout.Foldout(showHealthRanges, "HealthRanges");
        if(showHealthRanges)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Range"))
            {
                healthRanges.Add(new BP_HealthThreasholds.HealthRanges());
            }
            if (healthRanges.Count > 0)
            {
                if (GUILayout.Button("Remove Range"))
                {
                    healthRanges.RemoveAt(healthRanges.Count - 1);
                }
            }
            GUILayout.EndHorizontal();
            int newCount = healthRanges.Count;
            if (newCount > 0)
            {
                newCount = Mathf.Max(0, EditorGUILayout.IntField("Size", healthRanges.Count));
            }
            if (healthRanges.Count > 0)
            {
                for (int i = 0; i < healthRanges.Count; i++)
                {


                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    GUILayout.BeginVertical();

                    GUILayout.Label("Range:" + (i + 1));
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    healthRanges[i].phase = EditorGUILayout.ObjectField("Phase:", healthRanges[i].phase, typeof(BossPhase), true) as BossPhase;
                    //healthRanges[i].phase = hr_Phases[i];
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    healthRanges[i].threashhold = EditorGUILayout.Slider("Health Percentage", healthRanges[i].threashhold, 0f, 1f);
                    //healthRanges[i].threashhold = hr_Threashold[i];
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Sort by Percentage"))
                {
                    SortHeathRangeByPercentage();
                }
                
            }
        }
    }
    void SortHeathRangeByPercentage()
    {
        healthRanges = healthRanges.OrderByDescending(x => x.threashhold).ToList();
    }

    public static void ResizeList<T>(int size, List<T> list, T item)
    {
        while (size < list.Count)
            list.RemoveAt(list.Count - 1);
        while (size > list.Count)
            list.Add(item);
    }
}
