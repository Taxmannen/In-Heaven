using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_PatternPatternShot : BossAttack
{
    public PatternPatternShotData data;
    protected override IEnumerator Execute(Boss boss)
    {
        GameObject inst = new GameObject("BA_PatternShot for BA_PatternPatternShot");
        inst.transform.parent = transform;
        BA_PatternShot attack = inst.AddComponent(typeof(BA_PatternShot)) as BA_PatternShot;
        yield return null;
        foreach (var item in data.patternShotDatas)
        {
            attack.SetAttackData(item);
            yield return null;
            attack.StartExecute(boss);
            yield return new WaitUntil(() => attack.GetExecuteRoutine() == null);
        }
        

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

