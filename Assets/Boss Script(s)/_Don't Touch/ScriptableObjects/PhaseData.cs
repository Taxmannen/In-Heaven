﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Phase", menuName = "Boss/Phase")]
public class PhaseData : ScriptableObject
{
    public List<BossAttackTypeAndData> attacks = new List<BossAttackTypeAndData>();
    public List<BossMovementTypeAndData> movments = new List<BossMovementTypeAndData>();
}
[System.Serializable]
public class BossAttackTypeAndData
{
    public int type;
    public AttackData data;
}

[System.Serializable]
public class BossMovementTypeAndData
{
    public int type;
    public MovementData data;
}