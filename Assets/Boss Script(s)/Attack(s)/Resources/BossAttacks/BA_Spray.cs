using System.Collections;
using UnityEngine;

/* Script made by Daniel */
public class BA_Spray : BossAttack
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitIndicator;
    public SprayData data;
    private float startTime;
    private bool prevWasParry;
    private Transform spawnPoint;

    private Vector3 offset = new Vector3(0, 1, 0);

    protected override IEnumerator Execute(Boss boss)
    {
        yield return new WaitForSeconds(data.delayBetweenAttacks);
        hitIndicator.Play();
        spawnPoint = GameObject.Find("PatternShotList").GetComponentInChildren<Transform>();
        startTime = Time.time;
        InvokeRepeating("FireBullet", 0, data.fireRate);
        yield return new WaitForSeconds(data.attackDuration);
        hitIndicator.Stop();
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
                ShootingHelper.Shoot(spawnPoint.position, GameController.instance.playerController.transform.position + offset, BossBulletObjectPool.current.GetPooledPlasmaBulletParrable(), data.speed, BossBulletObjectPool.current.transform);
                prevWasParry = true;
            }
            else ShootingHelper.Shoot(spawnPoint.position, GameController.instance.playerController.transform.position + offset, BossBulletObjectPool.current.GetPooledPlasmaBullet(), data.speed, BossBulletObjectPool.current.transform);
        }
        else
        {
            ShootingHelper.Shoot(spawnPoint.position, GameController.instance.playerController.transform.position + offset, BossBulletObjectPool.current.GetPooledPlasmaBullet(), data.speed,BossBulletObjectPool.current.transform);
            prevWasParry = false;
        }

        //Play sound here
        AudioController.instance.BossPatternShot();
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