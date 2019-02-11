using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Planned by: Jesper Uddefors + Filip Nilsson
/// </summary>
public class BP_AttackThenMove : BossPhase
{

    private int attackIndex;
    private int movementIndex;

    protected override IEnumerator PhaseRoutine(Boss boss)
    {

        while (true)
        {

            if (bossAttacks.Count > 0)
            {

                attackRoutine = bossAttacks[attackIndex].StartExecute(boss);

                yield return new WaitUntil(() => bossAttacks[attackIndex].GetExecuteRoutine() == null);

                if (++attackIndex >= bossAttacks.Count)
                {
                    attackIndex = 0;
                }

            }


            if (bossMovements.Count > 0)
            {

                bossMovements[movementIndex].StartExecute(boss);

                yield return new WaitUntil(() => bossMovements[movementIndex].GetExecuteRoutine() == null);

                if (++movementIndex >= bossMovements.Count)
                {
                    movementIndex = 0;
                }

            }

            yield return null;

        }

    }

}
