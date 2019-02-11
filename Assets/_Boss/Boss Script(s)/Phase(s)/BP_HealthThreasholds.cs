using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class BP_HealthThreasholds : BossPhase
{
    
    public List<HealthRanges> bossPhases = new List<HealthRanges>();

    int c = 0;
    protected override IEnumerator PhaseRoutine(Boss boss)
    {
        SetupPhases();
        while (true)
        {


            //Start Phase
            bossPhases[c].phase.StartPhase(boss,movementParent,attackParent);

            yield return new WaitUntil(() => boss.hP <= boss.maxHP * bossPhases[c].threashhold);

            //Stop Phase
            bossPhases[c].phase.StopPhase();
            c++;
            yield return null;
        }

    }
    void SetupPhases()
    {

    }
      

    [System.Serializable]
    public class HealthRanges
    {
        
        public BossPhase phase;
        public float threashhold;
        public HealthRanges()
        {
            this.phase = new BossPhase();
            this.threashhold = 0;
        }
        public HealthRanges(BossPhase phase,float threashhold)
        {
            this.phase = phase;
            this.threashhold = threashhold;
        }
    }
}