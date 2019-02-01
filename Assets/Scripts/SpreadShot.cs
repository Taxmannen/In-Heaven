//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{

    private BossController boss;

    public GameObject SpreadShotProjectile;
    public GameObject SpreadShotBullet;

    [SerializeField] private Transform spreadShotBullets;
    [SerializeField] [Range(1, 100)] private float spreadShotFireRate;

    public float SpreadShotProjectileSpeed;
    public float SpreadShotBulletSpeed;

    public int numberOfBullets;
    
    public Vector3[] shotArray;
    
    private Coroutine bossShootSpreadShot;
    float timer;




    public Vector3 generateSpreadShotSpawn()
    {

        Vector3 SpreadShotBulletSpawnPosition = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(4.0f, 13.0f), 0);
                
        return SpreadShotBulletSpawnPosition;
    }

    public float generateSpreadShootTarget()
    {
        float spreadShotStartingTarget = Random.Range(-12f, 0f);

        return spreadShotStartingTarget;
    }




    public void SpreadShotShoot(Vector3 SpreadShotBulletSpawnPosition, float spreadShotStartingTarget)
    {
        if (bossShootSpreadShot == null)
        {           
            bossShootSpreadShot = StartCoroutine(SpreadShotCorutine(SpreadShotBulletSpawnPosition, spreadShotStartingTarget));
        }
    }

    
  

    private IEnumerator SpreadShotCorutine(Vector3 SpreadShotBulletSpawnPosition, float spreadShotStartingTarget)
    {
  
        
        Debug.Log(SpreadShotBulletSpawnPosition);
        float x = 0;
        for (int i = 0; i < numberOfBullets; i++){
            
            GameObject spreadShotClone = Instantiate(SpreadShotBullet, SpreadShotBulletSpawnPosition, Quaternion.identity, spreadShotBullets);

            Destroy(spreadShotClone, 10f);

            Vector3 SpreadShotTarget = new Vector3(spreadShotStartingTarget+ x, 0, 0) - SpreadShotBulletSpawnPosition;


            SpreadShotTarget.Normalize();

            x = x + 5;

            spreadShotClone.GetComponent<Rigidbody>().velocity = SpreadShotTarget * SpreadShotBulletSpeed;

            InterfaceController.instance.BossBulletOverlay(new Vector3(spreadShotStartingTarget + x, 0, 0));

            spreadShotClone.GetComponent<BossBullet>().SetDamage(1);
            
        }

        
       yield return new WaitForSeconds(spreadShotFireRate);
        bossShootSpreadShot = null;
        yield break;

    }

}
