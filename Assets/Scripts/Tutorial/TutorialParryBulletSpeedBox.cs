using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialParryBulletSpeedBox : MonoBehaviour
{
    public static TutorialParryBulletSpeedBox instance;
    internal Coroutine lowerBulletSpeedRoutine = null;
    internal Coroutine increaseBulletSpeedRoutine = null;
    public Coroutine instantiateBulletRoutine = null;
    internal Coroutine resetBulletRoutine = null;
    [SerializeField] TutorialCannon tutorialCannon;



    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TutorialBullet")
        {
            if (lowerBulletSpeedRoutine == null)
            {
                lowerBulletSpeedRoutine = StartCoroutine(LowerBulletSpeed());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TutorialBullet")
        {

            if (increaseBulletSpeedRoutine == null)
            {

                if(lowerBulletSpeedRoutine != null)
                {
                    StopCoroutine(lowerBulletSpeedRoutine);
                }
                
                increaseBulletSpeedRoutine = StartCoroutine(IncreaseBulletSpeed());
            }
            StartShoot();
            lowerBulletSpeedRoutine = null;
           
        }
    }

    private IEnumerator LowerBulletSpeed()
    {
        while (tutorialCannon.rigi != null)
        {
            tutorialCannon.LowerSpeed();
                if (tutorialCannon.bulletSpeed < 2)
                {
                    tutorialCannon.bulletSpeed = 2;
                    break;
                }
            yield return new WaitForSeconds(0.01f);
        }
        
        yield break;
    }
    private IEnumerator IncreaseBulletSpeed()
    {
        while (tutorialCannon.bulletSpeed < tutorialCannon.originalBulletSpeed)
        {
            tutorialCannon.IncreaseSpeed();
            if(tutorialCannon.bulletSpeed > tutorialCannon.originalBulletSpeed)
            {
                tutorialCannon.bulletSpeed = tutorialCannon.originalBulletSpeed;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        increaseBulletSpeedRoutine = null;
        yield break;
    }
    public void StopCoroutines()
    {
        StopCoroutine(IncreaseBulletSpeed());
        StopCoroutine(LowerBulletSpeed());

        tutorialCannon.bulletSpeed = tutorialCannon.originalBulletSpeed;

        increaseBulletSpeedRoutine = null;
        lowerBulletSpeedRoutine = null;
    }
    public void NullCoroutinesStatic()
    {
        StopCoroutine(IncreaseBulletSpeed());
        
        increaseBulletSpeedRoutine = null;
        lowerBulletSpeedRoutine = null;
    }
    public void StartShoot()
    {

        if (TutorialController.instance.superChargeResource.superCharge >= TutorialController.instance.superChargeResource.superChargeMax)
        {
            StopAllCoroutines();
            return;
        }

        if (instantiateBulletRoutine == null)
        {
            instantiateBulletRoutine = StartCoroutine(InstantiateBulletRoutine());
        }
        
    }
    public IEnumerator InstantiateBulletRoutine()
    {
        StopCoroutines();
        tutorialCannon.bulletSpeed = tutorialCannon.originalBulletSpeed;
        tutorialCannon.SpawnBullet();
        yield return new WaitForSeconds(1f);
        instantiateBulletRoutine = null;        
        yield break;
    }



}

