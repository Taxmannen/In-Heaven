using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
[System.Serializable]
public class BossPhase : MonoBehaviour
{

    //Serialized

    [SerializeField] protected List<BossMovement> movementPrefabs;
    [SerializeField] protected List<BossAttack> attackPrefabs;



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

            foreach (BossMovement bossMovement in movementPrefabs)
            {
                bossMovements.Add(Instantiate(bossMovement, this.movementParent));
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

            bossAttacks.Clear();

            foreach (BossAttack attack in attackPrefabs)
            {
                bossAttacks.Add(Instantiate(attack, this.attackParent));
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

    }

}