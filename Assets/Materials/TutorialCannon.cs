using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCannon : MonoBehaviour
{
    //public static TutorialCannon instance;
    public GameObject tutorialBulletPrefab;
    public Transform tutorialBulletOriginpos;
    public Transform tutorialBulletTargetPos;
    public Rigidbody rigi;
    public Vector3 direction;

    GameObject tutorialBulletClone;
    float originalBulletSpeed = 40;
    public float bulletSpeed = 40;

    private void Start()
    {
        bulletSpeed = originalBulletSpeed;
        direction = tutorialBulletTargetPos.position - tutorialBulletOriginpos.position;
    }

    public void SpawnBullet()
    {
        tutorialBulletClone = Instantiate(tutorialBulletPrefab, tutorialBulletOriginpos.position, tutorialBulletOriginpos.rotation);
        rigi = tutorialBulletClone.GetComponent<Rigidbody>();
        rigi.velocity = direction.normalized * bulletSpeed;

    }

    public void LowerSpeed()
    {
        rigi.velocity = direction.normalized * bulletSpeed--;
    }
    public void IncreaseSpeed()
    {
        rigi.velocity = direction.normalized * bulletSpeed++;
    }

}
