using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Delay : BossPhase
{

    [SerializeField] private float delay;

    protected override IEnumerator PhaseRoutine(Boss boss)
    {

        InterfaceController.instance.HideBossHPBar();
        yield return new WaitForSeconds(delay);
        boss.Die();
        yield break;

    }

}
