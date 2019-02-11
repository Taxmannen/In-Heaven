using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BA_PatternShot : BossAttack
{

    [SerializeField] private float duration;
    [SerializeField] private float delayAfterEachPattern;
    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject patternPrefab;
    private GameObject pattern;
    private float counter;
    private float deltaTime;

    [SerializeField] private float delayAfterAttack;
    //Variables

    protected override IEnumerator Execute(Boss boss)
    {
        counter = 0;
        //Code
        
        // Starting by creating the Pattern to a real GameObject
        pattern = Instantiate(patternPrefab, transform);

        // Here i get the Data from that object
        List<PatternStruct> targetLocations = pattern.GetComponent<PatternImporter>().patternList;
        targetLocations = targetLocations.OrderBy(x => x.timeDelay).ToList();
        //Debug.Log(targetLocations.ToString());
        
        // This variable will be used later
        float lastTimeUpdate = Time.time;
        while (counter < duration)
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
                Vector3 target = new Vector3(item.x, item.y);
                GameObject bullet = ShootingHelper.Shoot(spawnLocation + transform.position, target, bulletPrefab, 10, transform, 10);

                // This line will change, we are going to make a GeneralBullet, that checks what it collides with and
                // what it can damage. And if the shoot is parrable
                bullet.GetComponent<Bullet>().SetDamage(1);
                if (item.parryable)
                {
                    // Set the bullet to be parriable 
                }

                //Make the appriorite sound
                InterfaceController.instance.BossBulletOverlay(target);
                AudioController.instance.BossPatternShot();
            }
            counter += Time.time - lastTimeUpdate;
            //Debug.Log(counter);
            yield return new WaitForSeconds(delayAfterEachPattern);
        }

        // Here will be the delay between AttackTypes, if they are after each other.
        yield return new WaitForSeconds(delayAfterAttack);
        executeRoutine = null;
        yield break;

    }
}
