using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{

    protected Coroutine executeRoutine = null;

    public void StartSpawn(Boss boss)
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
