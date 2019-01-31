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
    public float bulletLifeTime = 2f;
    [SerializeField] internal Transform bulletParent;

    private void Start()
    {
        originalBulletSpeed = 40;
        bulletSpeed = originalBulletSpeed;
        direction = tutorialBulletTargetPos.position - tutorialBulletOriginpos.position;
      
    }

    public void SpawnBullet()
    {
        //speedBox.StopCoroutines();
        tutorialBulletClone = Instantiate(tutorialBulletPrefab, tutorialBulletOriginpos.position, tutorialBulletOriginpos.rotation, bulletParent);
        Destroy(tutorialBulletClone, bulletLifeTime);
        
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
        //rigi = tutorialBulletClone.GetComponent<Rigidbody>();
        Debug.Log(rigi);
        if (rigi != null)
        {
            rigi.velocity = direction.normalized * bulletSpeed++;
        }
    }
    public void ResetSpeed()
    {
        originalBulletSpeed = 40;
        bulletSpeed = originalBulletSpeed;
    }


}
