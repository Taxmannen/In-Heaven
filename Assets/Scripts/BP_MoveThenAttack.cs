using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class BP_MoveThenAttack : BossPhase
{

    private int movementIndex;
    private int attackIndex;
    

    protected override IEnumerator PhaseRoutine(Boss boss)
    {

        while (true)
        {

            if (bossMovements.Count > 0)
            {

                bossMovements[movementIndex].StartExecute(boss);

                yield return new WaitUntil(() => bossMovements[movementIndex].GetExecuteRoutine() == null);

                if (++movementIndex >= bossMovements.Count)
                {
                    movementIndex = 0;
                }

            }

            if (bossAttacks.Count > 0)
            {

                attackRoutine = bossAttacks[attackIndex].StartExecute(boss);

                yield return new WaitUntil(() => bossAttacks[attackIndex].GetExecuteRoutine() == null);

                if (++attackIndex >= bossAttacks.Count)
                {
                    attackIndex = 0;
                }

            }

            yield return null;

        }

    }

}
