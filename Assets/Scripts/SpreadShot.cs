//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    //private PlayerLocationTracker PlayerLocation;
    private BossController bossController;

    //public GameObject SpreadShotProjectile;
    public GameObject SpreadShotBullet;

    [SerializeField] private Transform spreadShotBullets;

    //public float SpreadShotProjectileSpeed;
    //public float SpreadShotBulletSpeed;

    public int numberOfBullets;
    private float spawnedBullets;
  
    public Vector3[] shotArray;

    [SerializeField] [Range(1, 100)] private float delayAfterCompleteSpread;

    private Coroutine bossShootSpreadShot;

    //private float timer;

    private float timer;

    public Vector3 generateSpreadShotSpawn()
    {
   Vector3 SpreadShotBulletSpawnPosition = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(4.0f, 13.0f), 0);

        return SpreadShotBulletSpawnPosition;
    }






    public void SpreadShotShoot(Vector3 SpreadShotBulletSpawnPosition)
    {
       
           
       

       
        if (bossShootSpreadShot == null)
        {
           
            bossShootSpreadShot = StartCoroutine(SpreadShotCorutine(SpreadShotBulletSpawnPosition));
        }
    }


  

    private IEnumerator SpreadShotCorutine(Vector3 SpreadShotBulletSpawnPosition)
    {
  
        
        Debug.Log(SpreadShotBulletSpawnPosition);
        for (int i = 0; i < 4; i++){
            int x = 0;
            GameObject spreadShotClone = Instantiate(SpreadShotBullet, SpreadShotBulletSpawnPosition, Quaternion.identity, spreadShotBullets);

        //for (int i = 0; i < 4; i++){

            // Vector3 SpreadShotTarget =  new Vector3(-10+(5 * x), 0, 0) - FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().position;
            Vector3 SpreadShotTarget = new Vector3(-10 + (5 * x), 0, 0) - SpreadShotBulletSpawnPosition;


        //    //Destroy(spreadShotClone, 3f);

        //    Vector3 SpreadShotTarget = new Vector3(-10+(5 * x), 0, 0);
        //    bossController.Shoot(SpreadShotTarget);


        //    x = x +  5;

            spreadShotClone.GetComponent<BossBullet>().SetDamage(90);
            
        }

        //    //spreadshotclone.getcomponent<bossbullet>().setdamage(40);

        //}

        

   
        
        yield return new WaitForSeconds(spreadShotFireRate);
        bossShootSpreadShot = null;
        yield break;
    }

   //IEnumerator MoveBulletsCorutine()
   // {

   //     SpreadShotBullet.GetComponent<Rigidbody>().velocity = new Vector3(0+(spawnedBullets*10),0+(spawnedBullets*10),0);

   //     yield break;
   // }

    //Vector3 spawnCircle(Vector3 originPoint, float radius)
    //{
    //    float degrees = spawnedBullets * 30;

    //    Vector3 bulletSpawn;

    //    bulletSpawn.x = originPoint.x + radius * Mathf.Sin(degrees);
    //    bulletSpawn.y = originPoint.y + radius * Mathf.Cos(degrees);
    //    bulletSpawn.z = originPoint.z;

    //    return bulletSpawn;
    //}

}
