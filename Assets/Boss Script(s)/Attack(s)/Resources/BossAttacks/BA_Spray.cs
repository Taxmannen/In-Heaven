using System.Collections;
using UnityEngine;

/* Script made by Daniel */
public class BA_Spray : BossAttack
{
    [SerializeField] private ParticleSystem muzzleFlash;
    public SprayData data;
    private float startTime;
    private bool prevWasParry;
    private Transform spawnPoint;

    protected override IEnumerator Execute(Boss boss)
    {
        Debug.Log("nu?");
        yield return new WaitForSeconds(data.delayBetweenAttacks);
        spawnPoint = GameObject.Find("PatternShotList").GetComponentInChildren<Transform>();
        startTime = Time.time;
        InvokeRepeating("FireBullet", 0, data.fireRate);
        yield return new WaitForSeconds(data.attackDuration);
        executeRoutine = null;
        yield break;
    }

    //This code runs every time a bullet spawns
    void FireBullet()
    {
        muzzleFlash.Play();
        if (Time.time - startTime >= data.attackDuration) CancelInvoke("FireBullet");

        if (data.anyParrableShots && !prevWasParry)
        {
            if (Random.Range(0, 100) < data.percentageOfShootsParrable)
            {
                ShootingHelper.Shoot(spawnPoint.position, GameController.instance.playerController.transform.position, BossBulletObjectPool.current.GetPooledPlasmaBulletParrable(), data.speed);
                prevWasParry = true;
            }
            else ShootingHelper.Shoot(spawnPoint.position, GameController.instance.playerController.transform.position, BossBulletObjectPool.current.GetPooledPlasmaBullet(), data.speed);
        }
        else
        {
            ShootingHelper.Shoot(spawnPoint.position, GameController.instance.playerController.transform.position, BossBulletObjectPool.current.GetPooledPlasmaBullet(), data.speed);
            prevWasParry = false;
        }

        //Play sound here
        //Add UI element for bullet here (if needed)
    }

    public override void SetAttackData(AttackData data)
    {
        if (this.data = data as SprayData)
        {
            //Debug.Log("SetAttackData");
        }
        else
        {
            Debug.LogError("Wrong Data!!");
        }
    }
}