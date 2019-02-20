using UnityEngine;

[CreateAssetMenu(fileName = "SprayData", menuName = "Boss/Spray")]
public class SprayData : AttackData
{
    [SerializeField] internal bool anyParrableShots;
    [SerializeField] internal float percentageOfShootsParrable;
    [SerializeField] internal float attackDuration;
    [SerializeField] internal float speed;
    [SerializeField] internal float fireRate;
    [SerializeField] internal float delayBetweenAttacks;
}
