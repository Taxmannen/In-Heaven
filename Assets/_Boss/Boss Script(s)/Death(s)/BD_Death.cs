﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BD_Death : BossDeath
{

    protected override IEnumerator Execute(Boss boss)
    {
        //Destroy(boss.gameObject);
        AudioController.instance.BossDeath();
        executeRoutine = null;
        Debug.Log("Dead");
        yield break;
    }

}
