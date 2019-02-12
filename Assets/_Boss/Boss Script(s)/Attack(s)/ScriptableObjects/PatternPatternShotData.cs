using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternPatternShotData", menuName = "Boss/PatternPatternShotData")]
public class PatternPatternShotData : AttackData
{
    [SerializeField] internal List<AttackData> patternShotDatas = new List<AttackData>();

}
