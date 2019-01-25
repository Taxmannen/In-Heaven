//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    public GameObject SpreadShotProjectile;
    public GameObject SpreadShotBullet;

    [SerializeField] private Transform spreadShotBullets;

    public float SpreadShotProjectileSpeed;
    public float SpreadShotBulletSpeed;

    public int numberOfBullets;
    
    public Vector3[] shotArray;

    [SerializeField] [Range(1, 100)] private float spreadShotFireRate;

    private Coroutine bossShootSpreadShot;

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

            Destroy(spreadShotClone, 3f);

            Vector3 SpreadShotTarget = new Vector3(-10 + (5 * x), 0, 0) - SpreadShotBulletSpawnPosition;


            SpreadShotTarget.Normalize();

            x = x + 5;

            spreadShotClone.GetComponent<Rigidbody>().velocity = SpreadShotTarget * SpreadShotBulletSpeed;

            InterfaceController.instance.BossBulletOverlay(FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().position);

            spreadShotClone.GetComponent<BossBullet>().SetDamage(90);
            
        }

        
        yield return new WaitForSeconds(spreadShotFireRate);
        bossShootSpreadShot = null;
        yield break;

    }

}
