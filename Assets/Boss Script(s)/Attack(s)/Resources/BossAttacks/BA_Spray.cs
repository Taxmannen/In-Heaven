using System.Collections;
using UnityEngine;

/* Script made by Daniel */
public class BA_Spray : BossAttack
{
    #region Variables
    [Header("Bullet")]
    [Range(0.5f, 10f)] [Tooltip("The duration of the attack")]
    [SerializeField] private float attackDuration = 5f;
    [Range(0.01f, 0.5f)] [Tooltip("How often the bullets will spawn")]
    [SerializeField] private float fireRate = 0.2f;
    [Range(5, 30)] [Tooltip("The movement speed of the bullet")]
    [SerializeField] private float speed = 20f;
    [Range(1, 10)] [Tooltip("How long until the bullet gets destroyed")]
    [SerializeField] private float destroyTime = 3f;

    [Header("Parrable")]
    [SerializeField] [Tooltip("If the attack should contain any parrable bullet")]
    private bool anyParrableShots = true;
    [Range(0, 100)]
    [SerializeField] [Tooltip("The chance of parrable bullet getting spawned")]
    private float percentageOfShootsParrable = 10f;

    [Header("Setup")]
    [SerializeField] [Tooltip("The prefab of the normal bullet")]
    private GameObject normalBullet;
    [SerializeField] [Tooltip("The prefab of the parrable bullet")]
    private GameObject parrableBullet;
    [SerializeField] [Tooltip("Where the bullets should spawn from")]
    private Transform spawnPoint;
    [SerializeField] [Tooltip("The parent object")]
    private Transform parent;

    [Header("DEBUG")]
    [Tooltip("For testing")] [SerializeField]
    private bool debugMode = false;
    [Tooltip("For testing")] [SerializeField]
    private float invokeDelay = 5;

    private float startTime;
    private bool prevWasParry;
    #endregion

    private void Start()
    {
        if (debugMode) InvokeRepeating("Test", 0, attackDuration + invokeDelay); 
    }

    void Test()
    {
        StartCoroutine(Execute(new Boss()));
    }

    protected override IEnumerator Execute(Boss boss)
    {
        startTime = Time.time;
        InvokeRepeating("FireBullet", 0, fireRate);

        executeRoutine = null;
        yield break;
    }

    //This code runs every time a bullet spawns
    void FireBullet()
    {
        if (Time.time - startTime >= attackDuration) CancelInvoke("FireBullet");

        GameObject bullet;
        if (anyParrableShots && !prevWasParry)
        {
            if (Random.Range(0, 100) < percentageOfShootsParrable)
            {
                bullet = parrableBullet;
                prevWasParry = true;
            }
            else bullet = normalBullet;
        }
        else
        {
            bullet = normalBullet;
            prevWasParry = false;
        }

        ShootingHelper.Shoot(spawnPoint.position, GameController.instance.playerController.transform.position, bullet, speed, parent, destroyTime);

        //Play sound here
        //Add UI element for bullet here (if needed)
    }
}