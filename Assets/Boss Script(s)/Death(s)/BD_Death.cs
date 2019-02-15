using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BD_Death : BossDeath
{

    protected override IEnumerator Execute(Boss boss)
    {
        //Destroy(boss.gameObject);
        AudioController.instance.BossDeath();
        Debug.Log("Dead");
        executeRoutine = null;
        yield break;
    }

}
