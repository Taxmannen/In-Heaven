using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{

    protected Coroutine executeRoutine = null;

    public void StartDeath(Boss boss)
    {
        executeRoutine = StartCoroutine(Execute(boss));
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

}
