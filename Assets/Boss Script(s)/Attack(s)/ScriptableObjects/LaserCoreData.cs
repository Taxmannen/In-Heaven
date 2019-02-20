using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserCoreData", menuName = "Boss/LaserCore")]
public class LaserCoreData : AttackData
{
    [SerializeField]
    internal GameObject laserPrefab;

    [SerializeField]
    internal float speed = 10;

    [SerializeField]
    public Vector3 startPosition = new Vector3(0, 0, 0);

    [SerializeField]
    public Vector3 endPosition = new Vector3(0, 0, 0);

}