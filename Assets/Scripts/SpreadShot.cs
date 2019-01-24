using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    private PlayerLocationTracker PlayerLocation;

    public GameObject SpreadShotProjectile;
    public GameObject SpreadShotBullet;

    public float SpreadShotProjectileSpeed;
    public float SpreadShotBulletSpeed;

    public int numberOfBullets;
    private float spawnedBullets;
    
    private float spreadShotLocationX;
    private float spreadShotLocationY;
    private float spreadShotLocationZ;

    private IEnumerator corutine;
    private IEnumerator corutine2;

    private float timer;

    void Start()
    {
        corutine = MoveBulletsCorutine();
        corutine2 = MoveProjectile();
    }

    
    void Update()
    {

        //PlayerLocation = GameObject.FindObjectOfType<PlayerLocationTracker>();
        //float PlayerVectorX = PlayerLocationTracker.playerLocationX;

        timer += Time.deltaTime;

        if (timer >= 10)
        {
            SpreadShootFire();
            timer = 0;
        }

    }

    void SpreadShootFire()
    {
       StartCoroutine(MoveProjectile());
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

  

    IEnumerator MoveProjectile()
    {

        Instantiate(SpreadShotProjectile, new Vector3(0, 5, 50), transform.rotation);
        SpreadShotProjectile.GetComponent<Rigidbody>().velocity = Vector3.back;

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
