using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserCoreData", menuName = "Boss/LaserCore")]
public class LaserCoreData : AttackData
{
    [SerializeField]
    internal GameObject laserPrefab;

    [SerializeField]
    internal float attackTime = 5f;

    [SerializeField]
    internal Vector3 speed = new Vector3(10, 0, 0);

    internal List<GameObject> lasers;

    [SerializeField]
    public Vector3 startPosition = new Vector3(0, 0, 0);

    [SerializeField]
    public Vector3 endPosition = new Vector3(0, 0, 0);

    [SerializeField]
    internal float laserStayTime = 5f;
}