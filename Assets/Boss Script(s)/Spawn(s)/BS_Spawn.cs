using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Spawn : BossSpawn
{
    public float timeBeforeWeShowHealth = 4f;
    protected override IEnumerator Execute(Boss boss)
    {
        InterfaceController.instance.HideBossHPBar();
        InterfaceController.instance.HidePlayerHealth();

        yield return new WaitForSeconds(timeBeforeWeShowHealth);
        InterfaceController.instance.ShowBossHPBar();
        InterfaceController.instance.ShowPlayerHealth();


        
        executeRoutine = null;
        yield break;
    }


}
