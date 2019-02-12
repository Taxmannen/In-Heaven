using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
[System.Serializable]
public class BossPhase : MonoBehaviour
{

    //Serialized

    public PhaseData data;



    //Protected

    protected Transform attackParent;
    protected Transform movementParent;
    protected Coroutine phaseRoutine = null;
    protected Coroutine movementRoutine = null;
    protected Coroutine attackRoutine = null;
    protected List<BossMovement> bossMovements = new List<BossMovement>();
    protected List<BossAttack> bossAttacks = new List<BossAttack>();



    //Main

    public void StartPhase(Boss boss, Transform movementParent, Transform attackParent)
    {

        SetupMovements(movementParent);

        SetupAttacks(attackParent);

        if (phaseRoutine == null)
        {
            phaseRoutine = StartCoroutine(PhaseRoutine(boss));
        }

    }

    protected void SetupMovements(Transform movementParent)
    {

        if (movementParent)
        {
            this.movementParent = movementParent;
        }
        
        else
        {

            GameObject movementObject;
            //Comment: Replace
            if (movementObject = GameObject.FindWithTag("Movement Parent"))
            {

                if (this.movementParent = movementObject.transform)
                {

                }

            }
        }

        if (this.movementParent)
        {

            bossMovements.Clear();
            if (data != null)
            {
                MonoScript[] scripts;
                scripts = Resources.LoadAll<MonoScript>("BossMovement/");



                foreach (BossMovementTypeAndData data in data.movments)
                {

                    GameObject inst = new GameObject();
                    inst.transform.parent = this.movementParent;
                    inst.transform.position = transform.position;
                    BossMovement movement = inst.AddComponent(scripts[data.type].GetClass()) as BossMovement;
                    movement.SetMovmentData(data.data);
                    bossMovements.Add(movement);
                }
            }
            

        }

    }

    protected void SetupAttacks(Transform attackParent)
    {

        if (attackParent)
        {
            this.attackParent = attackParent;
        }
        
        else
        {

            GameObject attackObject;
            //Comment: Replace
            if (attackObject = GameObject.FindWithTag("Attack Parent"))
            {

                if (this.attackParent = attackObject.transform)
                {

                }

            }
        }

        if (this.attackParent)
        {

            if (data != null)
            {
                bossAttacks.Clear();
                MonoScript[] scripts;
                scripts = Resources.LoadAll<MonoScript>("BossAttacks/");

                foreach (BossAttackTypeAndData data in data.attacks)
                {

                    GameObject inst = new GameObject();
                    inst.transform.parent = this.attackParent;
                    inst.transform.position = transform.position;
                    BossAttack attack = inst.AddComponent(scripts[data.type].GetClass()) as BossAttack;
                    attack.SetAttackData(data.data);
                    attack.name = attack.GetType().ToString();
                    bossAttacks.Add(attack);
                }
            }

        }

    }

    protected virtual IEnumerator PhaseRoutine(Boss boss)
    {
        phaseRoutine = null;
        yield break;
    }

    public void StopPhase()
    {

        if (phaseRoutine != null)
        {
            StopCoroutine(phaseRoutine);
            phaseRoutine = null;
        }
        if (attackRoutine != null)
        {
            foreach (var item in bossAttacks)
            {
                
                item.StopCoroutine(item.GetExecuteRoutine());
            }
            attackRoutine = null;
        }
        if (movementRoutine != null)
        {
            foreach (var item in bossAttacks)
            {
                item.StopCoroutine(item.GetExecuteRoutine());
            }
            
            movementRoutine = null;
        }

    }

}