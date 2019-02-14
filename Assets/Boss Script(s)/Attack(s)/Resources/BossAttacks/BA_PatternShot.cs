using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BA_PatternShot : BossAttack
{
    public PatternShotData data;

    public GameObject bulletPrefab;
    public GameObject parryableBulletPrefab;

    private GameObject pattern;
    private float counter;
    private float deltaTime;

    
    //Variables

    protected override IEnumerator Execute(Boss boss)
    {
        counter = 0;
        //Code
        //Debug.Log("Run Pattern Shot");
        // Starting by creating the Pattern to a real GameObject
        pattern = Instantiate(data.patternPrefab, transform);

        // Here i get the Data from that object
        List<PatternStruct> targetLocations = pattern.GetComponent<PatternImporter>().patternList;
        targetLocations = targetLocations.OrderBy(x => x.timeDelay).ToList();
        //Debug.Log(targetLocations.ToString());
        
        // This variable will be used later
        float lastTimeUpdate = Time.time;
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


                //Then use the data from the item.
                Vector3 target = new Vector3(item.x, item.y + 4);
                GameObject bullet;
                if (item.parryable)
                {
                    bullet = ShootingHelper.Shoot(data.spawnLocation + transform.position, target, parryableBulletPrefab, data.bulletSpeed, boss.bulletParent, 10);// Set the bullet to be parriable 
                }
                else
                {
                    bullet = ShootingHelper.Shoot(data.spawnLocation + transform.position, target, bulletPrefab, data.bulletSpeed, transform, 10);
                }


                // This line will change, we are going to make a GeneralBullet, that checks what it collides with and
                // what it can damage. And if the shoot is parrable
                bullet.GetComponent<Bullet>().SetDamage(1);
                bullet.GetComponent<Bullet>().SetBulletOverlay(data.spawnLocation + transform.position, target, data.bulletSpeed);

                

                //Make the appriorite sound
                //InterfaceController.instance.BossBulletOverlay(target);
                AudioController.instance.BossPatternShot();
            }
            counter++;
            //Debug.Log(counter);
            yield return new WaitForSeconds(data.delayAfterEachPattern);
        }

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

