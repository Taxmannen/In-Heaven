﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class BossAttack : MonoBehaviour
{

    //Protected Static

    protected Coroutine executeRoutine;



    //Main

    public Coroutine StartExecute(Boss boss)
    {

        if (executeRoutine == null)
        {
            executeRoutine = StartCoroutine(Execute(boss));
        }
        return executeRoutine;

    }

    protected virtual IEnumerator Execute(Boss boss)
    {
        executeRoutine = null;
        yield break;
    }

    public Coroutine GetExecuteRoutine()
    {
        return executeRoutine;
    }
    public virtual void SetAttackData(AttackData data)
    {

    }

}

