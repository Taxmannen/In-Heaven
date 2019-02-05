using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class BP_Random : BossPhase
{ 
    protected override IEnumerator PhaseRoutine(Boss boss)
    {



        while (true)
        {

            if (bossMovements.Count > 0)
            {

                int randomMovement = Random.Range(0, bossMovements.Count);

                bossMovements[randomMovement].StartExecute(boss);

            }

            if (bossAttacks.Count > 0)
            {

                int randomAttack = Random.Range(0, bossAttacks.Count);

                bossAttacks[randomAttack].StartExecute(boss);

            }

            yield return null;

        }

    }

}
