//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    private PlayerLocationTracker PlayerLocation;

    public GameObject SpreadShotProjectile;
    public GameObject SpreadShotBullet;

    [SerializeField] private Transform spreadShotBullets;

    public float SpreadShotProjectileSpeed;
    public float SpreadShotBulletSpeed;

    public int numberOfBullets;
    private float spawnedBullets;
    
    private float spreadShotLocationX;
    private float spreadShotLocationY;
    private float spreadShotLocationZ;

    public Vector3[] shotArray;

    [SerializeField] [Range(1, 100)] private float spreadShotFireRate;

    private Coroutine bossShootCoroutine;

    private IEnumerator corutine;
    private IEnumerator corutine2;

    private float timer;

    void Start()
    {
      
    }

    
   

  

    void SpreadShotDetonate(Vector3 originPoint)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector3 bulletSpawn = spawnCircle(originPoint, 1f);
            spawnedBullets = spawnedBullets + 1;

            Quaternion rotation = Quaternion.Euler(0, 10, 10);

            Instantiate(SpreadShotBullet, bulletSpawn, rotation);
            StartCoroutine(MoveBulletsCorutine());
        }
    }

  

    public IEnumerator SpreadShotCorutine()
    {
        Debug.Log("Shit seems to be working");
        Vector3 bossBulletSpawnPosition = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(4.0f, 13.0f), 0);

        for (int i = 0; i < 4; i++){
            int x = 0;
            GameObject spreadShotClone = Instantiate(SpreadShotBullet, bossBulletSpawnPosition, Quaternion.identity, spreadShotBullets);

            Destroy(spreadShotClone, 3f);

            Vector3 SpreadShotTarget = new Vector3(-10+(5 * x), 0, 0);

          

            SpreadShotTarget.Normalize();

            x = x + 5;

            spreadShotClone.GetComponent<Rigidbody>().velocity = SpreadShotTarget * SpreadShotBulletSpeed;

            InterfaceController.instance.BossBulletOverlay(FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().position);

            spreadShotClone.GetComponent<BossBullet>().SetDamage(40);

        }

        

   
        
        yield return new WaitForSeconds(5 / spreadShotFireRate);
        bossShootCoroutine = null;
        yield break;


       /*

        Instantiate(SpreadShotProjectile, new Vector3(0, 5, 50), transform.rotation);
        //SpreadShotProjectile.GetComponent<Rigidbody>().velocity = new Vector3.back;

        if (SpreadShotProjectile.GetComponent<Rigidbody>().position.z == 0)
        {
            spreadShotLocationX = SpreadShotProjectile.GetComponent<Rigidbody>().position.x;
            spreadShotLocationY = SpreadShotProjectile.GetComponent<Rigidbody>().position.y;
            spreadShotLocationZ = SpreadShotProjectile.GetComponent<Rigidbody>().position.z;

            Vector3 originPoint = transform.position;

            //  SpreadShotDetonate(Vector3 originPoint);
            for (int i = 0; i < numberOfBullets; i++)
            {
                Vector3 bulletSpawn = spawnCircle(originPoint, 1f);
                spawnedBullets = spawnedBullets + 1;

                Quaternion rotation = Quaternion.Euler(0, 10, 10);

                Instantiate(SpreadShotBullet, bulletSpawn, rotation);
                StartCoroutine(MoveBulletsCorutine());
            }

        }
        yield break;
        */
    }

   IEnumerator MoveBulletsCorutine()
    {

        SpreadShotBullet.GetComponent<Rigidbody>().velocity = new Vector3(0+(spawnedBullets*10),0+(spawnedBullets*10),0);

        yield break;
    }

    Vector3 spawnCircle(Vector3 originPoint, float radius)
    {
        float degrees = spawnedBullets * 30;

        Vector3 bulletSpawn;

        bulletSpawn.x = originPoint.x + radius * Mathf.Sin(degrees);
        bulletSpawn.y = originPoint.y + radius * Mathf.Cos(degrees);
        bulletSpawn.z = originPoint.z;

        return bulletSpawn;
    }

}
