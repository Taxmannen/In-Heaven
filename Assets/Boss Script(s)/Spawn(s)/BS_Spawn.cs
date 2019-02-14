using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Spawn : BossSpawn
{

    protected override IEnumerator Execute(Boss boss)
    {
        executeRoutine = null;
        yield break;
    }


}
