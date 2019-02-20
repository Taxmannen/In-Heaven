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
    public List<BossMovement> bossMovements = new List<BossMovement>();
    public List<BossAttack> bossAttacks = new List<BossAttack>();

    public BossAttack[] attackList;
    public BossMovement[] movementsList;

    //Main

    public void StartPhase(Boss boss, Transform movementParent, Transform attackParent)
    {
        movementsList = boss.movementScriptTransfromList.GetComponentsInChildren<BossMovement>();
        attackList = boss.attackScriptTransfromList.GetComponentsInChildren<BossAttack>();
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
                foreach (BossMovementTypeAndData data in data.movments)
                {

                    GameObject inst = Instantiate(movementsList[data.type].gameObject, this.movementParent);
                    inst.transform.position = transform.position;
                    BossMovement movement = inst.GetComponent(movementsList[data.type].GetType()) as BossMovement;
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

                foreach (BossAttackTypeAndData data in data.attacks)
                {

                    GameObject inst = Instantiate(attackList[data.type].gameObject, this.attackParent);
                    inst.transform.position = transform.position;
                    BossAttack attack = inst.GetComponent(attackList[data.type].GetType()) as BossAttack;
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
                if(item.GetExecuteRoutine() != null)
                item.StopCoroutine(item.GetExecuteRoutine());
            }
            attackRoutine = null;
        }
        if (movementRoutine != null)
        {
            foreach (var item in bossAttacks)
            {
                if (item.GetExecuteRoutine() != null)
                    item.StopCoroutine(item.GetExecuteRoutine());
            }
            
            movementRoutine = null;
        }
        gameObject.SetActive(false);

    }

}