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
    [SerializeField] TutorialParryBulletSpeedBox speedBox;
    GameObject tutorialBulletClone;
    public float originalBulletSpeed = 40;
    public float bulletSpeed = 40;

    private void Start()
    {
        bulletSpeed = originalBulletSpeed;
        direction = tutorialBulletTargetPos.position - tutorialBulletOriginpos.position;
      
    }

    public void SpawnBullet()
    {
        speedBox.StopCoroutines();
        tutorialBulletClone = Instantiate(tutorialBulletPrefab, tutorialBulletOriginpos.position, tutorialBulletOriginpos.rotation);
        rigi = tutorialBulletClone.GetComponent<Rigidbody>();
        ResetSpeed();
        rigi.velocity = direction.normalized * bulletSpeed;

    }

    public void LowerSpeed()
    {
        if (rigi != null)
        {
            rigi.velocity = direction.normalized * bulletSpeed--;
        }
    }
    public void IncreaseSpeed()
    {
        if (rigi != null)
        {
            rigi.velocity = direction.normalized * bulletSpeed++;
        }
    }
    public void ResetSpeed()
    {
        bulletSpeed = originalBulletSpeed;
        rigi.velocity = direction.normalized * bulletSpeed;

    }


}
