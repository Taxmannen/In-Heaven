using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PhaseData))]
public class PhaseDataEditor : Editor
{
    //Holding the current selected option in the Inspector
    List<int> attackIndex = new List<int>();
    List<int> movementIndex = new List<int>();
    List<AttackData> attackDatas = new List<AttackData>();
    List<MovementData> movementDatas = new List<MovementData>();

    //References from other Sources
    List<BossAttackTypeAndData> attacks;
    List<BossMovementTypeAndData> movements;
    BossAttack[] attackScripts;
    BossMovement[] movementScripts;
    PhaseData phaseData;

    //GUI
    bool showAttacks;
    bool showMovments;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        if(GUILayout.Button("Save"))
        {
            Save();
        }
        AttackList();
        MovementList();
    }

    private void OnEnable()
    {
        phaseData = target as PhaseData;

        attackScripts = GameObject.Find("AttackScriptsList").GetComponentsInChildren<BossAttack>();
        movementScripts = GameObject.Find("MovementScriptList").GetComponentsInChildren<BossMovement>();
        attacks = phaseData.attacks;
        movements = phaseData.movments;

        string[] attackStrings;
        attackStrings = new string[attackScripts.Length];

        for (int i = 0; i < attackStrings.Length; i++)
        {
            attackStrings[i] = attackScripts[i].GetType().ToString();
        }


        string[] movementStrings;
        movementStrings = new string[movementScripts.Length];

        for (int i = 0; i < movementStrings.Length; i++)
        {
            movementStrings[i] = movementScripts[i].GetType().ToString();
        }


        if (attacks != null)
        {
            ResizeList(phaseData.attacks.Count, attacks, new BossAttackTypeAndData());
            ResizeList(phaseData.attacks.Count, attackIndex, 0);
            ResizeList(phaseData.attacks.Count, attackDatas, null);
            for (int i = 0; i < attacks.Count; i++)
            {
                //Debug.Log("AttackData:" + phaseData.attacks[i].data);
                attackDatas[i] = phaseData.attacks[i].data;


                for (int j = 0; j < attackStrings.Length; j++)
                {
                    if (j == phaseData.attacks[i].type)
                    {
                        //Debug.Log("Matched Names: " + phaseData.attacks[i].type);
                        attackIndex[i] = j;
                    }
                }
            }
        }
        if (movements != null)
        {
            ResizeList(phaseData.movments.Count, movements, new BossMovementTypeAndData());
            ResizeList(phaseData.movments.Count, movementIndex, 0);
            ResizeList(phaseData.movments.Count, movementDatas, null);
            for (int i = 0; i < movements.Count; i++)
            {
                //Debug.Log("MovementData:" + phaseData.movments[i].data);
                movementDatas[i] = phaseData.movments[i].data;


                for (int j = 0; j < movementStrings.Length; j++)
                {
                    if (j == phaseData.movments[i].type)
                    {
                        //Debug.Log("Matched Names: " + phaseData.movments[i].type);
                        movementIndex[i] = j;
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        if(target != null)
        {
            Save();

        }
        
    }
    
    void Save()
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            phaseData.attacks[i].type = attackIndex[i];
            phaseData.attacks[i].data = attackDatas[i];
        }
        for (int i = 0; i < movements.Count; i++)
        {
            phaseData.movments[i].type = movementIndex[i];
            phaseData.movments[i].data = movementDatas[i];
        }
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
    
    void AttackList()
    {
        
        string[] vs;
        vs = new string[attackScripts.Length];

        for (int i = 0; i < vs.Length; i++)
        {
            vs[i] = attackScripts[i].GetType().ToString(); 
        }
        //List of Attacks
        showAttacks = EditorGUILayout.Foldout(showAttacks, "Attacks");

        if (showAttacks)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Attack"))
            {
                attacks.Add(new BossAttackTypeAndData());
            }

            if(attacks.Count > 0)
            {
                if (GUILayout.Button("Remove Attack"))
                {
                    attacks.RemoveAt(attacks.Count - 1);
                }
            }
            GUILayout.EndHorizontal();
            

            int newCount = Mathf.Max(0, EditorGUILayout.IntField("Size", attacks.Count));

            ResizeList(newCount, attacks, new BossAttackTypeAndData());
            ResizeList(newCount, attackIndex, 0);
            ResizeList(newCount, attackDatas, null);

            if (attacks.Count > 0)
            {
                for (int i = 0; i < attacks.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    GUILayout.Label("Attack:" + (i + 1));
                    if (vs.Length > 0)
                    {
                        attackIndex[i] = EditorGUILayout.Popup(attackIndex[i], vs);
                        if (attackScripts.Length > 0)
                        {
                            attacks[i].type = attackIndex[i];
                        }


                        attackDatas[i] = EditorGUILayout.ObjectField("", attackDatas[i], typeof(AttackData), false) as AttackData;
                        attacks[i].data = attackDatas[i];
                    }
                    else
                    {
                        GUILayout.Label("No Movment Script Found");
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }
    }
    
    void MovementList()
    {
        string[] vs;
        vs = new string[movementScripts.Length];

        for (int i = 0; i < vs.Length; i++)
        {
            vs[i] = movementScripts[i].GetType().ToString();
        }



        //List of Attacks
        showMovments = EditorGUILayout.Foldout(showMovments, "Movements");

        if (showMovments)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Movement"))
            {
                movements.Add(new BossMovementTypeAndData());
            }

            if (movements.Count > 0)
            {
                if (GUILayout.Button("Remove Movment"))
                {
                    movements.RemoveAt(movements.Count - 1);
                }
            }
            GUILayout.EndHorizontal();


            int newCount = Mathf.Max(0, EditorGUILayout.IntField("Size", movements.Count));

            ResizeList(newCount, movements, new BossMovementTypeAndData());
            ResizeList(newCount, movementIndex, 0);
            ResizeList(newCount, movementDatas, null);

            if (movements.Count > 0)
            {
                for (int i = 0; i < movements.Count; i++)
                {

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    GUILayout.Label("Move:" + (i + 1));
                    if (vs.Length > 0)
                    {
                        movementIndex[i] = EditorGUILayout.Popup(movementIndex[i], vs);
                        if (movementScripts.Length > 0)
                            movements[i].type = movementIndex[i];

                        movementDatas[i] = EditorGUILayout.ObjectField("", movementDatas[i], typeof(MovementData), false) as MovementData;
                        movements[i].data = movementDatas[i];
                    }
                    else
                    {
                        GUILayout.Label("No Movment Script Found");
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }
    }
    public static void ResizeList<T>(int size, List<T> list, T item)
    {
        while (size < list.Count)
            list.RemoveAt(list.Count - 1);
        while (size > list.Count)
            list.Add(item);
    }

}
