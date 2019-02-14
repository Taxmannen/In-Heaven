using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternShotData", menuName = "Boss/PatternShot")]
public class PatternShotData : AttackData
{
    [SerializeField] internal float numberOfPatternsToShoot = 1;
    [SerializeField] internal float delayAfterEachPattern = 1;
    [SerializeField] public Vector3 spawnLocation;
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] public GameObject patternPrefab;
    [SerializeField] internal float delayAfterAttack = 3;
    [SerializeField] internal float bulletSpeed = 400;


}
