    

    using System.Collections;

    public class BossPhaseExample : BossPhase
    {

        protected override IEnumerator PhaseRoutine(Boss boss)
        {

            phaseRoutine = null;
            yield break;

        }

    }







