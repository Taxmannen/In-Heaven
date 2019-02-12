using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternShotData", menuName = "Boss/PatternShot")]
public class PatternShotData : AttackData
{
    [SerializeField] internal float numberOfPatternsToShoot = 1;
    [SerializeField] internal float delayAfterEachPattern = 1;
    [SerializeField] internal Vector3 spawnLocation;
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] internal GameObject patternPrefab;
    [SerializeField] internal float delayAfterAttack = 3;
    [SerializeField] internal float bulletSpeed = 400;


}
