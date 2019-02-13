using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_PatternPatternShot : BossAttack
{
    public PatternPatternShotData data;
    private GameObject inst;
    BA_PatternShot attack;
    protected override IEnumerator Execute(Boss boss)
    {
        if(inst == null)
        {
            
            inst = Instantiate(boss.attackScriptTransfromList.GetComponentInChildren(typeof(BA_PatternShot)).gameObject, transform);
            inst.transform.position = transform.position;
            attack = inst.GetComponent<BA_PatternShot>();
            
        }
        yield return null;
        foreach (var item in data.patternShotDatas)
        {
            attack.SetAttackData(item);
            yield return null;
            attack.StartExecute(boss);
            yield return new WaitUntil(() => attack.GetExecuteRoutine() == null);
        }

        yield return new WaitForSeconds(data.sencondsAfterFinishAttack);

        executeRoutine = null;
        yield break;
    }
    public override void SetAttackData(AttackData data)
    {
        if (this.data = data as PatternPatternShotData)
        {
            //Debug.Log("SetAttackData");
        }
        else
        {
            Debug.LogError("Wrong Data!!");
        }
    }
}

