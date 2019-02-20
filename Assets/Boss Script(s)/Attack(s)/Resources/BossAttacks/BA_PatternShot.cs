using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BA_PatternShot : BossAttack
{
    public PatternShotData data;

    public GameObject bulletPrefab;
    public GameObject parryableBulletPrefab;

    public ParticleSystem muzzleFlash;

    public Animator animator;

    private GameObject pattern;
    private float counter;
    private float deltaTime;


    //Variables

    private void Start()
    {
        muzzleFlash = GameObject.Find("PS_BossMuzzleFlashSlow").GetComponent<ParticleSystem>();
        animator = GetComponentInParent<Boss>().GetComponentInChildren<Animator>();
    }

    protected override IEnumerator Execute(Boss boss)
    {
        counter = 0;
        animator.SetLayerWeight(3, 1);
        //Code
        //Debug.Log("Run Pattern Shot");
        // Starting by creating the Pattern to a real GameObject
        pattern = Instantiate(data.patternPrefab, transform);
        Transform[] listOfSpawnPoints = GameObject.Find("PatternShotList").GetComponentsInChildren<Transform>();
        // Here i get the Data from that object
        List<PatternStruct> targetLocations = pattern.GetComponent<PatternImporter>().patternList;
        targetLocations = targetLocations.OrderBy(x => x.timeDelay).ToList();
        //Debug.Log(targetLocations.ToString());
        int i = 0;
        // This variable will be used later
        float lastTimeUpdate = Time.time;
        AudioController.instance.PlayerCommenceShooting();
        while (counter < data.numberOfPatternsToShoot)
        {
            lastTimeUpdate = Time.time;
            float previousTime = 0;
            foreach (PatternStruct item in targetLocations)
            {

                // Here we check if the shot have a delay after the previous, they they are the same or lower, then they will appear at the same time. 
                if (item.timeDelay > previousTime)
                {
                    // Calulateing the delay between the patternshot. And then apply it to the Corutine
                    yield return new WaitForSeconds((item.timeDelay * 0.01f) - (previousTime * 0.01f));
                }
                previousTime = item.timeDelay;





                float speed = data.bulletSpeed;

                Vector3 origo = Vector3.zero;

                Vector3 spawn = listOfSpawnPoints[i++ % listOfSpawnPoints.Length].position;

                Vector3 target = new Vector3(item.x, item.y + 4);

                Vector3 offset = target - origo;

                Vector3 newDirection = (target - spawn) + offset;

                speed = (newDirection.magnitude / (target - spawn).magnitude) * speed;
                




                GameObject bullet;
                if (item.parryable)
                {
                    bullet = ShootingHelper.Shoot(spawn, target, BossBulletObjectPool.current.GetPooledPlasmaBulletParrable(), speed, boss.bulletParent, 10);
                    
                }
                else
                {
                    bullet = ShootingHelper.Shoot(spawn, target, BossBulletObjectPool.current.GetPooledPlasmaBullet(), speed, transform, 10);
                }


                // This line will change, we are going to make a GeneralBullet, that checks what it collides with and
                // what it can damage. And if the shoot is parrable
                bullet.GetComponent<Bullet>().SetDamage(1);
                bullet.GetComponent<Bullet>().isParrayable = item.parryable;
                bullet.GetComponent<Bullet>().SetBulletOverlay(spawn, target);

                

                //Make the appriorite sound
                //InterfaceController.instance.BossBulletOverlay(target);
                AudioController.instance.BossPatternShot();
                muzzleFlash.Play();
            }
            counter++;
            //Debug.Log(counter);
            yield return new WaitForSeconds(data.delayAfterEachPattern);
        }

        animator.SetLayerWeight(3, 0);
        // Here will be the delay between AttackTypes, if they are after each other.
        yield return new WaitForSeconds(data.delayAfterAttack);
        executeRoutine = null;
        yield break;

    }
    public override void SetAttackData(AttackData data)
    { 
        if(this.data = data as PatternShotData)
        {
            //Debug.Log("SetAttackData");
        }
        else
        {
            Debug.LogError("Wrong Data!!");
        }
    }

}

