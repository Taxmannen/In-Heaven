using UnityEngine;
using UnityEditor;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
[CustomEditor(typeof(Boss))]
public class BossEditor : Editor
{

    //Private

    private Boss boss;



    //Main

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        if (!boss)
        {
            boss = (Boss)target;
        }

        if (boss)
        {

            if (GUILayout.Button("Update BossHitboxElement(s)"))
            {
                boss.GetHitboxes();
            }

        }

    }

}
